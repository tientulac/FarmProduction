using FarmProductionAPI.Core.Queries.UserSite;
using FarmProductionAPI.Core.Queries.UserSiteQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmProductionAPI.Controllers.UserSite
{
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class UserSiteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserSiteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("category")]
        public async Task<ResponseResultAPI<List<ParentCategoryDTO>>> GetCategory(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetListCategoryQuery(), cancellationToken);
            return result;
        }

        [HttpGet]
        [Route("brand")]
        public async Task<ResponseResultAPI<List<BrandDTO>>> GetBrand(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetListBrandQuery(), cancellationToken);
            return result;
        }

        [HttpGet]
        [Route("product")]
        public async Task<ResponseResultAPI<List<ProductDTO>>> GetProduct(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetListProductQuery(), cancellationToken);
            return result;
        }

        [HttpPost]
        [Route("product-attribute")]
        public async Task<ResponseResultAPI<List<ProductAttributeDTO>>> GetProductAttribute([FromBody] GetListProductAttributeQuery command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpGet]
        [Route("attribute")]
        public async Task<ResponseResultAPI<AttributeDTO>> GetAttribute([FromQuery] GetListAttributeQuery command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
