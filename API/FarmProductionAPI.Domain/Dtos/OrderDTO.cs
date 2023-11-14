using FarmProductionAPI.Domain.Enums;
using FarmProductionAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Domain.Dtos
{
    public class OrderDTO
    {
        public Guid? Id { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsSoftDeleted { get; set; }
        public List<OrderItemDTO>? OrderItems { get; set; }
        public string? Code { get; set; }
        public StatusOrder? Status { get; set; }
        public OrderType? Type { get; set; }
        public Guid? UserAccountId { get; set; }
        public Guid? SellerAccountId { get; set; }
        public UserAccountDTO? UserAccount { get; set; }
        public UserAccountDTO? SellerAccount { get; set; }
        public decimal? Total { get; set; }
        public decimal? PaymentShip { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? WardFromId { get; set; }
        public string? DistrictFromId { get; set; }
        public string? ProvinceFromId { get; set; }
        public string? WardToId { get; set; }
        public string? DistrictToId { get; set; }
        public string? ProvinceToId { get; set; }
        public string? FromAddress { get; set; }
        public string? ToAddress { get; set; }
    }
}
