using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using OneOf;

namespace FarmProductionAPI.Core.Commands.RoleCommand
{
    public class SaveRoleCommand : ICommand<ResponseResultAPI<RoleDTO>>
    {
        public Guid? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }
    }
}
