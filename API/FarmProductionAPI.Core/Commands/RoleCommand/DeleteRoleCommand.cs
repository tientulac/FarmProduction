using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core.Commands.RoleCommand
{
    public record DeleteRoleCommand(Guid? Id) : ICommand<ResponseResultAPI<RoleDTO>>;
}
