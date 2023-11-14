using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Models
{
    public class UserAccount : BaseEntity
    {
        public Guid? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Hashpassword { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public StatusAccount? Status { get;set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }

    public enum StatusAccount
    {
        ACTIVE = 1,
        DISABLE,
        BAN,
        APPROVE,
        AWAITING_APPROVE
    }
}
