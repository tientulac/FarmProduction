using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Models
{
    public class ProductImage : BaseEntity
    {
        public Guid? ProductId { get; set; }
        public string? Image { get; set; }
        public virtual Product? Product { get; set; }
    }
}
