using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.UserSiteQuery;
public record GetListBrandQuery : IRequest<ResponseResultAPI<List<BrandDTO>>>;
