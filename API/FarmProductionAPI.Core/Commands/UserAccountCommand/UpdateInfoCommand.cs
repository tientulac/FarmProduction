using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
namespace FarmProductionAPI.Core.Commands.UserAccountCommand;

public class UpdateInfoCommand : ICommand<ResponseResultAPI<UserAccountDTO>>
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? FullName { get; set; }
    public string? Address { get; set; }
}