using MediatR;
using Microsoft.EntityFrameworkCore;
using Questrix.Application.Bases;
using Questrix.Application.DTOs;
using Questrix.Application.Exceptions;
using Questrix.Application.Interfaces.AutoMapper;
using Questrix.Application.Interfaces.Services;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Application.Features.Sessions.Commands.Start
{
    public class StartSessionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IInvitationService invitationService) : BaseHandler(mapper, unitOfWork), IRequestHandler<StartSessionCommandRequest, StartSessionCommandResponse>
    {
        private readonly IInvitationService invitationService = invitationService;

        public async Task<StartSessionCommandResponse> Handle(StartSessionCommandRequest request, CancellationToken cancellationToken)
        {
            Guid surveyId = await invitationService.ResolveSurveyIdAsync(request.InvitationCode, cancellationToken);
            Survey survey = (await unitOfWork.GetReadRepository<Survey>().GetAsync(s => s.Id == surveyId && !s.IsDeleted, cancellationToken, queryable => queryable.Include(s => s.Nodes))) ?? throw new SurveyNotFoundException();
            SurveyNode firstNode = survey.Nodes.First(sn => sn.Id == survey.StartNode);

            Session session = await unitOfWork.GetWriteRepository<Session>().AddAsync(new()
            {
                UserId = request.UserId,
                SurveyId = surveyId,
                CurrentNodeId = firstNode.Id,
                Status = "Active"
            }, cancellationToken);

            return new()
            {
                Id = session.Id,
                SurveyNode = mapper.Map<SurveyNodeDTO, SurveyNode>(firstNode)
            };
        }
    }
}
