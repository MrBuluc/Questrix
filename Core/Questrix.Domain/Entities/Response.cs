using Questrix.Domain.Common;

namespace Questrix.Domain.Entities
{
    public class Response : EntityBase<Guid>
    {
        public Guid SessionId { get; set; }
        public Guid NodeId { get; set; }
        public string Answer { get; set; }

        public Session Session { get; set; }
    }
}
