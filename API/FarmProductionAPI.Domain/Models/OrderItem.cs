using FarmProductionAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Models
{
    public class OrderItem : BaseEntity
    {
        public StatusOrder? Status { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductAttributeId { get; set; }
        public virtual Order? Order { get; set; }
        public virtual ProductAttribute? ProductAttribute { get; set; }
        public decimal? CountBought { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
