using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.ProductCommand
{
    public record DeleteProductCommand(Guid? Id) : ICommand<ResponseResultAPI<ProductDTO>>;

}
