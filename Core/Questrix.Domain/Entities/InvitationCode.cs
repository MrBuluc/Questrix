using Questrix.Domain.Common;

namespace Questrix.Domain.Entities
{
    public class InvitationCode : EntityBase<Guid>
    {
        public string Code { get; set; }
        public Guid SurveyId { get; set; }
        public int UsegeCount { get; set; }
        public int MaxUsege { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
