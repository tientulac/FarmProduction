using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.ProductAttributeCommand
{
    public class SaveProductAttributeCommand : ICommand<ResponseResultAPI<ProductAttributeDTO>>
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public string? Image { get; set; }
        public string? Color { get; set; }
        public string? Unit { get; set; }
        public string? Value { get; set; }
        public Guid? ProductId { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
