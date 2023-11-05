using FarmProductionAPI.Core.Commands.CategoryCommand;
using FarmProductionAPI.Core.Commands.ProductCommand;
using FarmProductionAPI.Core.Queries.ProductQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("get-by-request")]
        public async Task<ResponseResultAPI<List<ProductDTO>>> GetByRequest([FromQuery] GetListProductQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpGet]
        public async Task<ResponseResultAPI<List<ProductDTO>>> GetListProduct([FromQuery] GetListProductQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResultAPI<ProductDTO>> Save([FromBody] SaveProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ResponseResultAPI<ProductDTO>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return result;
        }
    }
}
