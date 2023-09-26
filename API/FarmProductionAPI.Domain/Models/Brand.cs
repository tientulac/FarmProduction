namespace FarmProductionAPI.Domain.Models
{
    public class Brand : BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
