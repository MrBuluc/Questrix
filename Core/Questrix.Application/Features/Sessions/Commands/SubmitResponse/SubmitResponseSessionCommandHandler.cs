using MediatR;
using Microsoft.EntityFrameworkCore;
using Questrix.Application.Bases;
using Questrix.Application.DTOs;
using Questrix.Application.Exceptions;
using Questrix.Application.Interfaces.AutoMapper;
using Questrix.Application.Interfaces.Services;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Application.Models;
using Questrix.Domain.Entities;
using System.Text.Json;

namespace Questrix.Application.Features.Sessions.Commands.SubmitResponse
{
    public class SubmitResponseSessionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ISurveyExecutionService surveyExecutionService, ITelegramTextValidator telegramTextValidator) : BaseHandler(mapper, unitOfWork), IRequestHandler<SubmitResponseSessionCommandRequest, SubmitResponseSessionCommandResponse>
    {
        private readonly ISurveyExecutionService surveyExecutionService = surveyExecutionService;
        private readonly ITelegramTextValidator telegramTextValidator = telegramTextValidator;

        public async Task<SubmitResponseSessionCommandResponse> Handle(SubmitResponseSessionCommandRequest request, CancellationToken cancellationToken)
        {
            Session session = (await unitOfWork.GetReadRepository<Session>().GetAsync(s => s.UserId == request.UserId && s.Status == "Active" && !s.IsDeleted, cancellationToken)) ?? throw new SessionNotFoundException();

            Survey survey = (await unitOfWork.GetReadRepository<Survey>().GetAsync(s => s.Id == session.SurveyId && !s.IsDeleted, cancellationToken, include: queryable => queryable.Include(s => s.Nodes)))
                ?? throw new SurveyNotFoundException();
            SurveyNode currentNode = (await unitOfWork.GetReadRepository<SurveyNode>().GetAsync(sn => sn.Id == session.CurrentNodeId && !sn.IsDeleted, cancellationToken, queryable => queryable.Include(sn => sn.Rules)
            .Include(sn => sn.Options)))!;

            
            if (!telegramTextValidator.ValidateNonTextInput(currentNode, request.Answer))
                throw new MessageNotValidateException();

            await unitOfWork.GetWriteRepository<Response>().AddAsync(new()
            {
                SessionId = session.Id,
                NodeId = currentNode.Id,
                Answer = JsonSerializer.Serialize(new Answer
                {
                    Value = request.Answer
                })
            }, cancellationToken);

            Guid? nextNodeId = surveyExecutionService.ResolveNextNode(currentNode, request.Answer);
            SurveyNode? nextNode = null;

            if (nextNodeId is null)
            {
                session.Status = "Completed";

                InvitationCode invitationCode = (await unitOfWork.GetReadRepository<InvitationCode>().GetAsync(ic => ic.SurveyId == survey.Id && !ic.IsDeleted, cancellationToken)) ?? throw new InvitationCodeNotFoundBySurveyIdException(survey.Id);
                invitationCode.UsegeCount++;

                await unitOfWork.GetWriteRepository<InvitationCode>().UpdateAsync(invitationCode);
            }
            else
            {
                session.CurrentNodeId = nextNodeId.Value;
                nextNode = (await unitOfWork.GetReadRepository<SurveyNode>().GetAsync(sn => sn.Id == nextNodeId && !sn.IsDeleted, cancellationToken, queryable => queryable.Include(sn => sn.Rules)
            .Include(sn => sn.Options)))!;
            }

            await unitOfWork.GetWriteRepository<Session>().UpdateAsync(session);

            await unitOfWork.SaveAsync(cancellationToken);

            mapper.Map<SurveyOptionDTO, SurveyOption>(new SurveyOption());
            mapper.Map<SurveyRuleDTO, SurveyRule>(new SurveyRule());

            return nextNodeId is null ? new()
            {
                IsCompleted = true
            } : new()
            {
                IsCompleted = false,
                SurveyNode = mapper.Map<SurveyNodeDTO, SurveyNode>(nextNode!)
            };
        }
    }
}
