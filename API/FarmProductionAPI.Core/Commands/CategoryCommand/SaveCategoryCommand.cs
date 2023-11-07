using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using OneOf;

namespace FarmProductionAPI.Core.Commands.CategoryCommand
{
    public class SaveCategoryCommand : ICommand<ResponseResultAPI<CategoryDTO>>
    {
        public Guid? Id { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
    }
}
