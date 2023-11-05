using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.ProductDescriptionQuery;

public record GetListProductDescriptionQuery : IRequest<ResponseResultAPI<List<ProductDescriptionDTO>>>
{
    public string? SearchStringKeyword { get; set; } 
    public Guid? ProductId { get; set; }
}

