using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.UserAccountQuery
{
    public record GetListUserAccountQuery : IRequest<ResponseResultAPI<List<UserAccountDTO>>>;
}
