using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.UserAccountQuery
{
    public record GetListUserAccountQuery : IRequest<ResponseResultAPI<List<UserAccountDTO>>>
    {
        public Guid? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
    }
}
