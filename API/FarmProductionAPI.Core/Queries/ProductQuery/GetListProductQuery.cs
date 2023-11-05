using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.ProductQuery;

public record GetListProductQuery : IRequest<ResponseResultAPI<List<ProductDTO>>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Image { get; set; }
    public List<Guid>? BrandIds { get; set; }
    public List<Guid>? CategoryIds { get; set; }
}
