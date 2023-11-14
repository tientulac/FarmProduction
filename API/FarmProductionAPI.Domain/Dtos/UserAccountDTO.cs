using FarmProductionAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Dtos
{
    public class UserAccountDTO
    {
        public Guid? Id { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsSoftDeleted { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public RoleDTO? Role { get; set; }
        public string? Token { get; set; }
        public StatusAccount? Status { get; set; }
    }
}
