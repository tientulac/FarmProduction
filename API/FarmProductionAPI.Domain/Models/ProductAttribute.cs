namespace FarmProductionAPI.Domain.Models;
public class ProductAttribute : BaseEntity
{
    public string? Code { get; set; } 
    public Guid? ProductId { get; set; }
    public decimal? Price { get; set; }
    public int? Amount { get; set; }
    public string? Image { get; set; }
    public string? Color { get; set; }
    public string? Unit { get; set; }
    public string? Value { get; set; }
    public DateTime? ManufactureDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public virtual Product? Product { get; set; }
    public virtual ICollection<OrderItem>? OrderItems { get; set; }
}