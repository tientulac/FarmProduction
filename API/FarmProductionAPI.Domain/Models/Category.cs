namespace FarmProductionAPI.Domain.Models
{
    public class Category : BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
