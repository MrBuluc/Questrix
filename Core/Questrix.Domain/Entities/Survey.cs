using Questrix.Domain.Common;

namespace Questrix.Domain.Entities
{
    public class Survey : EntityBase<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public Guid StartNode { get; set; }

        public ICollection<SurveyNode> Nodes { get; set; }
    }
}
