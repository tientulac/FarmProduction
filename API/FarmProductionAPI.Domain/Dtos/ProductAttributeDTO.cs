using FarmProductionAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Dtos
{
    public class ProductAttributeDTO
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
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public int? AmountBought { get; set; }
        public string? Image { get; set; }
        public string? Color { get; set; }
        public string? Unit { get; set; }
        public string? Value { get; set; }
        public Guid? ProductId { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
