using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Enums;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;

namespace FarmProductionAPI.Core.Commands.OrderItemCommand
{
    public class SaveOrderItemCommand : ICommand<ResponseResultAPI<OrderItemDTO>>
    {
        public Guid? Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductAttributeId { get; set; }
        public StatusOrder? Status { get; set; }
        public decimal? CountBought { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
