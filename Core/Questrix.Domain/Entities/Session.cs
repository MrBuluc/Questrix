using Questrix.Domain.Common;

namespace Questrix.Domain.Entities
{
    public class Session : EntityBase<Guid>
    {
        public string UserId { get; set; }
        public Guid SurveyId { get; set; }
        public Guid CurrentNodeId { get; set; }
        public string Status { get; set; }

        public ICollection<Response> Responses { get; set; }
    }
}
