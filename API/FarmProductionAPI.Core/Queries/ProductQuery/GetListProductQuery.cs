using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.ProductQuery
{
    public record GetListProductQuery : IRequest<ResponseResultAPI<List<ProductDTO>>>;
}
