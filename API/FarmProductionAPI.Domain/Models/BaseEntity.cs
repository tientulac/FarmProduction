namespace FarmProductionAPI.Domain.Models
{
    public class BaseEntity
    {
        public Guid? Id { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsSoftDeleted { get; set; }
    }
}
