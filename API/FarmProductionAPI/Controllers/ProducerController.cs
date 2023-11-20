using FarmProductionAPI.Core.Commands.ExportCommand;
using FarmProductionAPI.Core.Commands.ProducerCommand;
using FarmProductionAPI.Core.Queries.ProducerQuery;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class ProducerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProducerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("get-by-request")]
        public async Task<ResponseResultAPI<List<ProducerDTO>>> GetByRequet([FromBody] GetListProducerQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpGet]
        public async Task<ResponseResultAPI<List<ProducerDTO>>> GetListProducer([FromQuery] GetListProducerQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResultAPI<ProducerDTO>> Save([FromBody] SaveProducerCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ResponseResultAPI<ProducerDTO>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteProducerCommand(id));
            return result;
        }
    }
}
