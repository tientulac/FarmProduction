using AutoMapper;
using FarmProductionAPI.Core.Queries.OrderItemQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.OrderItemHandler
{
    public class GetListOrderItemHandler : IRequestHandler<GetListOrderItemQuery, ResponseResultAPI<List<OrderItemDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<OrderItem> _repository;

        public GetListOrderItemHandler(IMapper mapper, ILogger logger, IRepository<OrderItem> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<OrderItemDTO>>> Handle(GetListOrderItemQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orderItems = _repository.GetAll();
                if (request.OrderId.HasValue)
                {
                    orderItems = orderItems.Where(x => x.OrderId == request.OrderId).Include(x => x.Order).Include(x => x.ProductAttribute);
                }
                return new ResponseResultAPI<List<OrderItemDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<OrderItemDTO>>(orderItems),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<OrderItemDTO>>()
                {
                    Code = "500",
                    Data = null,
                    Message = ex.Message,
                    MessageEX = ex.ToString()
                };
            }
        }
    }

}
