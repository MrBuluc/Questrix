using Questrix.Domain.Entities;

namespace Questrix.Application.DTOs
{
    public class SurveyNodeDTO
    {
        public Guid Id { get; set; }
        public SurveyNodeType Type { get; set; }
        public string Question { get; set; }
        public string? Metadata { get; set; }
        public IList<SurveyOptionDTO>? Options { get; set; }
        public IList<SurveyRuleDTO>? Rules { get; set; }
    }
}
