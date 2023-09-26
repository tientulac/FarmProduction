using FarmProductionAPI.Core.Commands.CategoryCommand;
using FarmProductionAPI.Core.Queries.CategoryQuery;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmProductionAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseResultAPI<List<CategoryDTO>>> GetListCategory([FromQuery] GetListCategoryQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseResultAPI<CategoryDTO>> Save([FromBody] SaveCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<ResponseResultAPI<CategoryDTO>> Delete(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand { Id = id });
            return result;
        }
    }
}
