using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;

namespace FarmProductionAPI.Core.Commands.UserAccountCommand
{
    public class SaveUserAccountCommand : ICommand<ResponseResultAPI<UserAccountDTO>>
    {
        public Guid? Id { get; set; }
        public Guid? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Hashpassword { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public StatusAccount? Status { get; set; }
    }
}
