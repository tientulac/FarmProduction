namespace FarmProductionAPI.Domain.Dtos;

public class ProductDescriptionDTO
{
    public Guid? Id { get; set; }
    public Guid? CreatedById { get; set; }
    public Guid? UpdatedById { get; set; }
    public Guid? DeletedById { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool? IsSoftDeleted { get; set; }
    public Guid? ProductId { get; set; }
    public string? Description { get; set; }
}