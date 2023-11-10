using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Enums;
using FarmProductionAPI.Domain.Response;
using MediatR;

namespace FarmProductionAPI.Core.Queries.OrderQuery;

public record GetListOrderQuery : IRequest<ResponseResultAPI<List<OrderDTO>>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public PaymentType? PaymentType { get; set; }
    public StatusOrder? StatusOrder { get; set; }
    public OrderType? OrderType { get; set; }
    public string? Code { get; set; }
    public string? UserName { get; set; }
};
