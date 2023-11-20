using AutoMapper;
using FarmProductionAPI.Core.Commands.OrderItemCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.OrderItemHandler
{
    public class InsertOrderItemHandler : ICommandHandler<InsertOrderItemCommand, ResponseResultAPI<List<OrderItemDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<OrderItem> _repository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;

        public InsertOrderItemHandler(
            IMapper mapper, 
            ILogger logger, 
            IRepository<OrderItem> repository,
            IRepository<ProductAttribute> productAttributeRepository,
            IUnitOfWork unitOfWork
        )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _productAttributeRepository = productAttributeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<OrderItemDTO>>> Handle(InsertOrderItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attributes = _productAttributeRepository.GetAll().Include(x => x.Product).AsQueryable().ToList();
                var orderItems = _repository.GetAll().Where(x => x.OrderId == request.OrderId && x.IsSoftDeleted != true).AsQueryable().ToList();

                if (!string.IsNullOrEmpty(request.ProductName))
                {
                    attributes = attributes.Where(x => x.Product.Name.ToLower().Contains(request.ProductName.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(request.Code))
                {
                    attributes = attributes.Where(x => x.Code.ToLower().Contains(request.Code.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(request.Color))
                {
                    attributes = attributes.Where(x => x.Color == request.Color).ToList();
                }
                var listAttIds = attributes.Select(x => x.Id);
                if (listAttIds.Any())
                {
                    foreach (var item in attributes)
                    {
                        var orderItem = orderItems.FirstOrDefault(x => x.ProductAttributeId == item.Id);
                        if (orderItem != null)
                        {
                            orderItem.CountBought += 1;
                            await _repository.Update(orderItem, orderItem);
                        }
                        else
                        {
                            await _repository.Add(new OrderItem
                            {
                                Status = 0,
                                OrderId = request.OrderId,
                                ProductAttributeId = item.Id,
                                CountBought = 1,
                                UnitPrice = item.Price
                            });
                        }
                        item.Amount -= 1;
                        await _productAttributeRepository.Update(item, item);
                    }
                    return new ResponseResultAPI<List<OrderItemDTO>>()
                    {
                        Code = "200",
                        Data = _mapper.Map<List<OrderItemDTO>>(orderItems),
                        Message = "Thành công",
                    };
                }
                else
                {
                    return new ResponseResultAPI<List<OrderItemDTO>>()
                    {
                        Code = "500",
                        Data = null,
                        Message = "Không tìm thấy sản phẩm nào",
                    };
                }
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
