using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.ProductDescriptionCommand;

public class SaveProductDescriptionCommand : ICommand<ResponseResultAPI<ProductDescriptionDTO>>
{
    public Guid? Id { get; set; }
    public Guid? ProductId { get; set; }
    public string? Description { get; set; }
}