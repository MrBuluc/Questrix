using Questrix.Domain.Common;

namespace Questrix.Domain.Entities
{
    public class SurveyRule : EntityBase<Guid>
    {
        public Guid NodeId { get; set; }
        public string Condition { get; set; }
        public Guid NextNodeId { get; set; }
        public bool IsDefault { get; set; }

        public SurveyNode Node { get; set; }
    }
}
