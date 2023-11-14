using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Enums;
using FarmProductionAPI.Domain.Response;
using OneOf;

namespace FarmProductionAPI.Core.Commands.CategoryCommand
{
    public class SaveProductCommand : ICommand<ResponseResultAPI<ProductDTO>>
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProducerId { get; set; }
        public ProductStatus? Status { get; set; }
    }
}
