using FarmProductionAPI.Domain.Enums;
using FarmProductionAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Dtos
{
    public class ProductDTO
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
        public string? Image { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProducerId { get; set; }
        public ProductStatus? Status { get; set; }
        public  CategoryDTO? Category { get; set; }
        public  BrandDTO? Brand { get; set; }
        public  ProducerDTO? Producer { get; set; }
        public List<ProductImageDTO>? ProductImages { get; set; }
        public List<ProductDescriptionDTO>? ProductDesciptions { get; set; }
    }
}
