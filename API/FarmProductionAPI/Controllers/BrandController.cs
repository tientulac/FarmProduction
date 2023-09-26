﻿using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Core.Queries.BrandQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class BrandController: ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseResultAPI<List<BrandDTO>>> GetListBrand([FromQuery] GetListBrandQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResultAPI<BrandDTO>> Save([FromBody] SaveBrandCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
