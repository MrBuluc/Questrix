namespace Questrix.Application.DTOs
{
    public class SurveyNodeDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public IList<SurveyOptionDTO> Options { get; set; }
        public IList<SurveyRuleDTO> Rules { get; set; }
    }
}
