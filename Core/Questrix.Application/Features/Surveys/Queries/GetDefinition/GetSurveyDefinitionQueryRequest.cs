using MediatR;

namespace Questrix.Application.Features.Surveys.Queries.GetDefinition
{
    public class GetSurveyDefinitionQueryRequest : IRequest<GetSurveyDefinitionQueryResponse>
    {
        public string InvitationCode { get; set; }
    }
}
