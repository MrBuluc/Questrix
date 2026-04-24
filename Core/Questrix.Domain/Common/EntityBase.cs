namespace Questrix.Domain.Common
{
    public class EntityBase<TKey> : IEntityBase
    {
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
