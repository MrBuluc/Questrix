using MediatR;
using Microsoft.EntityFrameworkCore;
using Questrix.Application.Exceptions;
using Questrix.Application.Interfaces.Services;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Application.Features.Sessions.Commands.SubmitResponse
{
    public class SubmitResponseSessionCommandHandler(IUnitOfWork unitOfWork, ISurveyExecutionService surveyExecutionService) : IRequestHandler<SubmitResponseSessionCommandRequest, SubmitResponseSessionCommandResponse>
    {
        private readonly ISurveyExecutionService surveyExecutionService = surveyExecutionService;

        public async Task<SubmitResponseSessionCommandResponse> Handle(SubmitResponseSessionCommandRequest request, CancellationToken cancellationToken)
        {
            Session session = (await unitOfWork.GetReadRepository<Session>().GetAsync(s => s.UserId == request.UserId && s.Status == "Active" && !s.IsDeleted, cancellationToken)) ?? throw new SessionNotFoundException();

            Survey survey = (await unitOfWork.GetReadRepository<Survey>().GetAsync(s => s.Id == session.SurveyId && !s.IsDeleted, cancellationToken, include: queryable => queryable.Include(s => s.Nodes)))
                ?? throw new SurveyNotFoundException();
            SurveyNode currentNode = survey.Nodes.First(sn => sn.Id == session.CurrentNodeId && !sn.IsDeleted);

            await unitOfWork.GetWriteRepository<Response>().AddAsync(new()
            {
                SessionId = session.Id,
                NodeId = currentNode.Id,
                Answer = request.Answer
            }, cancellationToken);

            Guid? nextNodeId = surveyExecutionService.ResolveNextNode(currentNode, request.Answer);
            string nextNodeQuestion = string.Empty;

            if (nextNodeId is null)
            {
                session.Status = "Completed";
            }
            else
            {
                session.CurrentNodeId = nextNodeId.Value;
                nextNodeQuestion = survey.Nodes.First(sn => sn.Id == session.CurrentNodeId && !sn.IsDeleted).Question;
            }

            await unitOfWork.GetWriteRepository<Session>().UpdateAsync(session);

            await unitOfWork.SaveAsync(cancellationToken);

            return nextNodeId is null ? new()
            {
                IsCompleted = true
            } : new()
            {
                IsCompleted = false,
                NextQuestion = nextNodeQuestion
            };
        }
    }
}
