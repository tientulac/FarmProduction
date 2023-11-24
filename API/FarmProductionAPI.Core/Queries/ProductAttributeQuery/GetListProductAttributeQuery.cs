using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

public record GetListProductAttributeQuery : IRequest<ResponseResultAPI<List<ProductAttributeDTO>>>
{
    public Guid? ProductId { get; set; }
    public string? Color { get; set; }
    public string? Unit { get; set; }
    public string? Value { get; set; }
    public decimal? Price { get; set; }
    public FarmProductionAPI.Core.Queries.ProductAttributeQuery.FilterPriceType? FilterType { get; set; }
};
