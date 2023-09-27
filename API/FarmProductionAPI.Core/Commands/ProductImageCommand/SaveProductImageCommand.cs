using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using OneOf;

namespace FarmProductionAPI.Core.Commands.ProductImageCommand
{
    public class SaveProductImageCommand : ICommand<ResponseResultAPI<ProductImageDTO>>
    {
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
        public string? Image { get; set; }
    }
}
