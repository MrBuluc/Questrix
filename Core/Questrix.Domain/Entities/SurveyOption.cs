using Questrix.Domain.Common;

namespace Questrix.Domain.Entities
{
    public class SurveyOption : EntityBase<Guid>
    {
        public Guid NodeId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }

        public SurveyNode Node { get; set; }
    }
}
