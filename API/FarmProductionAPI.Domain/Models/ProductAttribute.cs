using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Models
{
    public class ProductAttribute: BaseEntity
    {
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public string? Image { get; set; }
        public string? Color { get; set; }
        public string? Unit { get; set; }
        public string? Value  { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
