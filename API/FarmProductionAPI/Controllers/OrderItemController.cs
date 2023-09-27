using FarmProductionAPI.Core.Commands.OrderItemCommand;
using FarmProductionAPI.Core.Queries.OrderItemQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class OrderItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseResultAPI<List<OrderItemDTO>>> GetListOrderItem([FromQuery] GetListOrderItemQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResultAPI<OrderItemDTO>> Save([FromBody] SaveOrderItemCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ResponseResultAPI<OrderItemDTO>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteOrderItemCommand(id));
            return result;
        }
    }
}
