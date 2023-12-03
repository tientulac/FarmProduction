using AutoMapper;
using FarmProductionAPI.Core.Commands.UserSiteCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OfficeOpenXml.Style;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.UserSiteHandler
{
    public class CanclerOrderHandler : ICommandHandler<CancleOrderCommand, ResponseResultAPI<OrderDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Order> _repository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public CanclerOrderHandler(IMapper mapper, ILogger logger, IRepository<Order> repository, IUnitOfWork unitOfWork, IRepository<ProductAttribute> productAttributeRepository, IRepository<OrderItem> orderItemRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _productAttributeRepository = productAttributeRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<ResponseResultAPI<OrderDTO>> Handle(CancleOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _repository.GetById(request.Id.Value);
                if (order is not null)
                {
                    order.Status = Domain.Enums.StatusOrder.Cancle;
                    await _repository.Update(_mapper.Map<Order>(order), order);
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
                
                var orderItems = _orderItemRepository.GetAllUserSite().Where(x => x.OrderId == request.Id.Value).AsQueryable();
                if (orderItems.Any())
                {
                    foreach (var item in orderItems)
                    {
                        var att = _productAttributeRepository.GetAllUserSite().FirstOrDefault(x => x.Id == item.ProductAttributeId);
                        if (att != null)
                        {
                            att.Amount = (int?)(att.Amount + item.CountBought);
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
