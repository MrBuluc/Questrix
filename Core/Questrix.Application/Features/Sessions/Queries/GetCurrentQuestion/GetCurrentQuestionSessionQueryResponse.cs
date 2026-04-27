using Questrix.Application.DTOs;

namespace Questrix.Application.Features.Sessions.Queries.GetCurrentQuestion
{
    public class GetCurrentQuestionSessionQueryResponse
    {
        public string Question { get; set; }
        public string Type { get; set; }
        public IList<SurveyOptionDTO> Options { get; set; }
    }
}
