using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.UserSiteQuery;
public record GetListAttributeQuery : IRequest<ResponseResultAPI<AttributeDTO>> { 
    public Guid? ProductId { get; set; }
};

