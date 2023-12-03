using AutoMapper;
using FarmProductionAPI.Core.Commands.OrderCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public SaveOrderHandler(IMapper mapper, ILogger logger, IRepository<Order> repository, IUnitOfWork unitOfWork, IRepository<ProductAttribute> productAttributeRepository, IRepository<OrderItem> orderItemRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _productAttributeRepository = productAttributeRepository;
            _orderItemRepository = orderItemRepository;
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

                if (request.ListItem.Any() && request.ListItem.Count > 0)
                {
                    foreach (var item in request.ListItem)
                    {
                        await _orderItemRepository.Add(new OrderItem
                        {
                            Status = 0,
                            OrderId = order.Id,
                            ProductAttributeId = item.Id,
                            CountBought = item.AmountBought,
                            UnitPrice = item.Price
                        });
                        var att = _productAttributeRepository.GetAllUserSite().FirstOrDefault(x => x.Id == item.Id);
                        if (att != null)
                        {
                            att.Amount = att.Amount - item.AmountBought;
                            await _productAttributeRepository.Update(att, att);
                        }
                    }
                    return new ResponseResultAPI<OrderDTO>()
                    {
                        Code = "200",
                        Data = _mapper.Map<OrderDTO>(order),
                        Message = "Thành công",
                    };
                }

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
