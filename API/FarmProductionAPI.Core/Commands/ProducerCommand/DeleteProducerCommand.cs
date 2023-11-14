using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.ProducerCommand;

public record DeleteProducerCommand(Guid? Id) : ICommand<ResponseResultAPI<ProducerDTO>>;