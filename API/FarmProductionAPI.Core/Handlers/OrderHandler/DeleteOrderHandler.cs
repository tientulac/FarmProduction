using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.OrderCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.OrderHandler
{
    public class DeleteOrderHandler : ICommandHandler<DeleteOrderCommand, ResponseResultAPI<OrderDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Order> _repository;

        public DeleteOrderHandler(IMapper mapper, ILogger logger, IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<OrderDTO>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var Order = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (Order is not null)
                    {
                        await _repository.Remove(Order);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<OrderDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<OrderDTO>(Order),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<OrderDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<OrderDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<OrderDTO>()
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
