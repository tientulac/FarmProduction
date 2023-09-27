using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.UserAccountCommand
{
    public class LoginCommand : ICommand<ResponseResultAPI<UserAccountDTO>>
    {
        public string? UserName { get; set; }
        public string? Hashpassword { get; set; }
    }
}
