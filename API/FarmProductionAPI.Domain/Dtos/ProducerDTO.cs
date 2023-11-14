using FarmProductionAPI.Domain.Enums;
using FarmProductionAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Dtos
{
    public class ProducerDTO
    {
        public Guid? Id { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
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
        public List<ProductDTO>? Products { get; set; }
    }
}
