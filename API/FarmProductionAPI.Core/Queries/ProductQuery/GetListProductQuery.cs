using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.ProductQuery;

public record GetListProductQuery : IRequest<ResponseResultAPI<List<ProductDTO>>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? CategoryId { get; set; }

    //public List<Guid>? BrandIds { get; set; }
    //public List<Guid>? CategoryIds { get; set; }
}
