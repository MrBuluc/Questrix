namespace Questrix.Application.DTOs
{
    public class SurveyRuleDTO
    {
        public string Condition { get; set; }
        public Guid NextNodeId { get; set; }
        public bool IsDefault { get; set; }
    }
}
