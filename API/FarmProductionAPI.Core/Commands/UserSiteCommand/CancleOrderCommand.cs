using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;

namespace FarmProductionAPI.Core.Commands.UserSiteCommand
{
    public class CancleOrderCommand : ICommand<ResponseResultAPI<OrderDTO>>
    {
        public Guid? Id { get; set; }
    }
}
