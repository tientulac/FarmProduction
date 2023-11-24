using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.UserSite;

public record GetListCategoryQuery : IRequest<ResponseResultAPI<List<ParentCategoryDTO>>>;
