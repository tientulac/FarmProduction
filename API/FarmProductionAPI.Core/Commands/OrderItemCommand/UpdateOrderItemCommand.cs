using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.OrderItemCommand
{
    public class UpdateOrderItemCommand : ICommand<ResponseResultAPI<OrderItemDTO>>
    {
        public Guid? Id { get; set; }
        public Guid? ProductAttributeId { get; set; }
        public decimal? CountBought { get; set; }
    }
}
