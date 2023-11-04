using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using OneOf;
using System.Text.Json.Serialization;

namespace FarmProductionAPI.Core.Commands.BrandCommand
{
    public class SaveBrandCommand : ICommand<ResponseResultAPI<BrandDTO>>
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
    }
}
