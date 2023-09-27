using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.ProductAttributeQuery
{
    public record GetListProductAttributeQuery : IRequest<ResponseResultAPI<List<ProductAttributeDTO>>>;
}
