using FarmProductionAPI.Core.Commands.ExportCommand;
using FarmProductionAPI.Core.Commands.OrderCommand;
using FarmProductionAPI.Core.Queries.OrderQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.ExportModels;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]")]
    ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("get-by-request")]
        public async Task<ResponseResultAPI<List<OrderDTO>>> GetByRequest([FromBody] GetListOrderQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpGet]
        public async Task<ResponseResultAPI<List<OrderDTO>>> GetListOrder([FromQuery] GetListOrderQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResultAPI<OrderDTO>> Save([FromBody] SaveOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ResponseResultAPI<OrderDTO>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));
            return result;
        }

        [HttpGet]
        [Route("export")]
        public async Task<ResponseResultAPI<byte[]>> ExportOrder([FromQuery] ExportExcelCommand<OrderExport> command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
