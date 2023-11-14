namespace FarmProductionAPI.Domain.Dtos
{
    public class RoleDTO
    {
        public Guid? Id { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsSoftDeleted { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }
    }
}
