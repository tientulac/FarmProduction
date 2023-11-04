using AutoMapper;
using FarmProductionAPI.Core.Commands.OrderItemCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.OrderItemHandler
{
    public class SaveOrderItemHandler : ICommandHandler<SaveOrderItemCommand, ResponseResultAPI<OrderItemDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<OrderItem> _repository;

        public SaveOrderItemHandler(IMapper mapper, ILogger logger, IRepository<OrderItem> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<OrderItemDTO>> Handle(SaveOrderItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderItem = new OrderItem();
                if (request.Id.HasValue)
                {
                    orderItem = await _repository.GetById(request.Id.Value);
                    if (orderItem is not null)
                    {
                        await _repository.Update(_mapper.Map<OrderItem>(request), orderItem);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
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

                orderItem = _mapper.Map<OrderItem>(request);
                await _repository.CreateOneAsync(orderItem, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<OrderItemDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<OrderItemDTO>(orderItem),
                    Message = "Success"
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
