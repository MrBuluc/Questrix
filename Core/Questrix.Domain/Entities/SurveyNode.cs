using Questrix.Domain.Common;

namespace Questrix.Domain.Entities
{
    public class SurveyNode : EntityBase<Guid>
    {
        public Guid SurveyId { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string Metadata { get; set; }

        public Survey Survey { get; set; }
        public ICollection<SurveyOption> Options { get; set; }
        public ICollection<SurveyRule> Rules { get; set; }
    }
}
