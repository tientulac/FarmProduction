using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.ProductDescriptionCommand;

public record DeleteProductDescriptionCommand(Guid? Id) : ICommand<ResponseResultAPI<ProductDescriptionDTO>>;
