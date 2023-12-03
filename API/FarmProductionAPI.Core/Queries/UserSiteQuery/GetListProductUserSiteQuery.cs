using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.UserSiteQuery
{
    public record GetListProductUserSiteQuery : IRequest<ResponseResultAPI<List<ProductDTO>>>
    {
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? SearchString { get; set; }
    }

}
