using FarmProductionAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Models
{
    public class Product: BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        public ProductStatus? Status { get; set; }
        public Guid? ProducerId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Producer? Producer { get; set; }
        public virtual ICollection<ProductAttribute>? ProductAttributes { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
        public virtual ICollection<ProductDescription>? ProductDescriptions { get; set; }
    }
}
