using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.UserSiteQuery
{
    public record GetListProductQuery : IRequest<ResponseResultAPI<List<ProductDTO>>>
    {
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
    }

}
