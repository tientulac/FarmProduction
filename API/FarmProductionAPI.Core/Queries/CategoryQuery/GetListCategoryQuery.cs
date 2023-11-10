using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.CategoryQuery;
public record GetListCategoryQuery : IRequest<ResponseResultAPI<List<CategoryDTO>>>
{
    public string? Code { get; set; }
    public string? Name { get; set; }
};
