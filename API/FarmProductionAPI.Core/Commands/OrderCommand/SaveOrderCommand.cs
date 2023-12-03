using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Enums;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;

namespace FarmProductionAPI.Core.Commands.OrderCommand
{
    public class SaveOrderCommand : ICommand<ResponseResultAPI<OrderDTO>>
    {
        public Guid? Id { get; set; }
        public StatusOrder? Status { get; set; }
        public OrderType? Type { get; set; }
        public Guid? UserAccountId { get; set; }
        public Guid? SellerAccountId { get; set; }
        public decimal? Total { get; set; }
        public decimal? PaymentShip { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? Code { get; set; }
        public string? WardFromId { get; set; }
        public string? DistrictFromId { get; set; }
        public string? ProvinceFromId { get; set; }
        public string? WardToId { get; set; }
        public string? DistrictToId { get; set; }
        public string? ProvinceToId { get; set; }
        public string? FromAddress { get; set; }
        public string? ToAddress { get; set; }
        public List<ProductAttributeDTO>? ListItem { get; set; }
    }
}
