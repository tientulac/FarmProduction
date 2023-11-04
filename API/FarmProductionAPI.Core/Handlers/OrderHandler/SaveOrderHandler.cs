using AutoMapper;
using FarmProductionAPI.Core.Commands.OrderCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.OrderHandler
{
    public class SaveOrderHandler : ICommandHandler<SaveOrderCommand, ResponseResultAPI<OrderDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Order> _repository;

        public SaveOrderHandler(IMapper mapper, ILogger logger, IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<OrderDTO>> Handle(SaveOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = new Order();
                if (request.Id.HasValue)
                {
                    order = await _repository.GetById(request.Id.Value);
                    if (order is not null)
                    {
                        await _repository.Update(_mapper.Map<Order>(request), order);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
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

                order = _mapper.Map<Order>(request);
                await _repository.CreateOneAsync(order, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<OrderDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<OrderDTO>(order),
                    Message = "Success"
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
