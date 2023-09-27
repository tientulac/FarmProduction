﻿using FarmProductionAPI.Core.Commands.RoleCommand;
using FarmProductionAPI.Core.Queries.RoleQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseResultAPI<List<RoleDTO>>> GetListRole([FromQuery] GetListRoleQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResultAPI<RoleDTO>> Save([FromBody] SaveRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ResponseResultAPI<RoleDTO>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            return result;
        }
    }
}
