using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
namespace FarmProductionAPI.Core.Commands.ProductAttributeCommand
{
    public record DeleteProductAttributeCommand : ICommand<ResponseResultAPI<ProductAttributeDTO>>
    {
        public Guid? Id { get; set; }
    };
}
