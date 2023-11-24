using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.UserAccountCommand
{
    public class ChangePasswordCommand : ICommand<ResponseResultAPI<UserAccountDTO>>
    {
        public Guid? Id { get; set; }
        public string? NewHashpassword { get; set; }
    }
}
