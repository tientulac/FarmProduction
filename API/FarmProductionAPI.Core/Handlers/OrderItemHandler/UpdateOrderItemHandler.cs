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
    public class UpdateOrderItemHandler : ICommandHandler<UpdateOrderItemCommand, ResponseResultAPI<OrderItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<OrderItem> _repository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;

        public UpdateOrderItemHandler(
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

        public async Task<ResponseResultAPI<OrderItemDTO>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderItem = _repository.GetAll().FirstOrDefault(x => x.Id == request.Id);
                var att = _productAttributeRepository.GetAll().FirstOrDefault(x => x.Id == request.ProductAttributeId);
                var countDefault = att.Amount + orderItem.CountBought - request.CountBought;
                if (!(countDefault > 0))
                {
                    return new ResponseResultAPI<OrderItemDTO>()
                    {
                        Code = "500",
                        Data = _mapper.Map<OrderItemDTO>(orderItem),
                        Message = "Thất bại",
                    };
                }

                orderItem.CountBought = request.CountBought;
                await _repository.Update(orderItem, orderItem);

                att.Amount = (int)countDefault;
                await _productAttributeRepository.Update(att, att);

                return new ResponseResultAPI<OrderItemDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<OrderItemDTO>(orderItem),
                    Message = "Thành công",
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
