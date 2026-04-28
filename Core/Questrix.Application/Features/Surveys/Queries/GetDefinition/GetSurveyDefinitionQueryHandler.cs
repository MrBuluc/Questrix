using MediatR;
using Questrix.Application.Bases;
using Questrix.Application.Exceptions;
using Questrix.Application.Interfaces.AutoMapper;
using Questrix.Application.Interfaces.Services;
using Questrix.Application.Interfaces.UnitOfWorks;
using Questrix.Domain.Entities;

namespace Questrix.Application.Features.Surveys.Queries.GetDefinition
{
    public class GetSurveyDefinitionQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IInvitationService invitationService) : BaseHandler(mapper, unitOfWork), IRequestHandler<GetSurveyDefinitionQueryRequest, GetSurveyDefinitionQueryResponse>
    {
        private readonly IInvitationService invitationService = invitationService;

        public async Task<GetSurveyDefinitionQueryResponse> Handle(GetSurveyDefinitionQueryRequest request, CancellationToken cancellationToken)
        {
            Guid surveyId = await invitationService.ResolveSurveyIdAsync(request.InvitationCode, cancellationToken);

            return mapper.Map<GetSurveyDefinitionQueryResponse, Survey>((await unitOfWork.GetReadRepository<Survey>().GetAsync(s => s.Id == surveyId && !s.IsDeleted, cancellationToken)) ?? throw new SurveyNotFoundException());
        }
    }
}
