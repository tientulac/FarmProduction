using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.RoleQuery
{
    public record GetListRoleQuery : IRequest<ResponseResultAPI<List<RoleDTO>>>;
}
