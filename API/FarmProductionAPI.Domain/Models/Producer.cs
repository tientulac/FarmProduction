using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Models
{
    public class Producer : BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Image { get; set; }
        public TypeModel? TypeModel { get; set; }
        public string? MainMarketing { get; set; }
        public string? Description { get; set; }
        public int? YearBorn { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }

    public enum TypeModel
    {
        ECOMMERCE,
        IMPORT,
        EXPORT
    }
}
