using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.OrderItemCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.OrderItemHandler
{
    public class DeleteOrderItemHandler : ICommandHandler<DeleteOrderItemCommand, ResponseResultAPI<OrderItemDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<OrderItem> _repository;

        public DeleteOrderItemHandler(IMapper mapper, ILogger logger, IRepository<OrderItem> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<OrderItemDTO>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var OrderItem = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (OrderItem is not null)
                    {
                        await _repository.Remove(OrderItem);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<OrderItemDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<OrderItemDTO>(OrderItem),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<OrderItemDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<OrderItemDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<OrderItemDTO>()
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
