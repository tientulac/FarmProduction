using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.ProductImageQuery;

public record GetListProductImageQuery : IRequest<ResponseResultAPI<List<ProductImageDTO>>>
{
    public Guid? ProductId { get; set; }
}