using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.OrderItemQuery
{
    public record GetListOrderItemQuery : IRequest<ResponseResultAPI<List<OrderItemDTO>>>
    {
        public Guid? OrderId { get; set; }
    };
}
