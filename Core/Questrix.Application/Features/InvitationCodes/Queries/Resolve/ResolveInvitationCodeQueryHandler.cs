using MediatR;
using Microsoft.EntityFrameworkCore;
using Questrix.Application.Bases;
using Questrix.Application.DTOs;
using Questrix.Application.Exceptions;
using Questrix.Application.Interfaces.AutoMapper;
using Questrix.Application.Interfaces.Services;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Application.Features.InvitationCodes.Queries.Resolve
{
    public class ResolveInvitationCodeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IInvitationService invitationService) : BaseHandler(mapper, unitOfWork), IRequestHandler<ResolveInvitationCodeQueryRequest, ResolveInvitationCodeQueryResponse>
    {
        private readonly IInvitationService invitationService = invitationService;

        public async Task<ResolveInvitationCodeQueryResponse> Handle(ResolveInvitationCodeQueryRequest request, CancellationToken cancellationToken)
        {
            Guid surveyId = await invitationService.ResolveSurveyIdAsync(request.Code, cancellationToken);
            Survey survey = (await unitOfWork.GetReadRepository<Survey>().GetAsync(s => s.Id == surveyId && !s.IsDeleted, cancellationToken, include: queryable => queryable.Include(s => s.Nodes))) ?? throw new SurveyNotFoundException();

            mapper.Map<SurveyOptionDTO, SurveyOption>(new SurveyOption());
            mapper.Map<SurveyRuleDTO, SurveyRule>(new SurveyRule());
            mapper.Map<SurveyNodeDTO, SurveyNode>(new SurveyNode());

            return new()
            {
                Survey = mapper.Map<SurveyDTO, Survey>(survey)
            };
        }
    }
}
