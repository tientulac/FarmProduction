using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.ProductAttributeQuery;

public record GetListProductAttributeByRequestQuery : IRequest<ResponseResultAPI<List<ProductAttributeDTO>>>
{
    public Guid? ProductId { get; set; }
    public string? Color { get; set; }
    public string? Unit { get; set; }
    public string? Value { get; set; }
    public decimal? Price { get; set; }
    public FilterPriceType? FilterType { get; set; }
};

public enum FilterPriceType
{
    GREATER,
    EQUAL,
    LESS
};