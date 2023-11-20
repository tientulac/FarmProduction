using FarmProductionAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Dtos
{
    public class OrderItemDTO
    {
        public Guid? Id { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public Guid? DeletedById { get; set; }
        public Guid? ProductAttributeId { get; set; }
        public Guid? OrderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsSoftDeleted { get; set; }
        public StatusOrder? Status { get; set; }
        public ProductAttributeDTO? ProductAttribute { get; set; }
        public decimal? CountBought { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
