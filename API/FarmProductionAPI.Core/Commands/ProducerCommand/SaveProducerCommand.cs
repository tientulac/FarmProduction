using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.ProducerCommand
{
    public class SaveProducerCommand : ICommand<ResponseResultAPI<ProducerDTO>>
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Image { get; set; }
        public TypeModel? TypeModel { get; set; }
        public string? MainMarketing { get; set; }
        public string? Description { get; set; }
        public int? YearBorn { get; set; }
    }
}
