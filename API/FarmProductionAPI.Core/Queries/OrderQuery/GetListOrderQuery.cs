using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.OrderQuery
{
    public record GetListOrderQuery : IRequest<ResponseResultAPI<List<OrderDTO>>>;
}
