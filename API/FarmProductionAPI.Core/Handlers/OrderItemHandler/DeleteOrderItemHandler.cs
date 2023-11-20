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
        private readonly IRepository<ProductAttribute> _productAttributeRepository;

        public DeleteOrderItemHandler(
            IMapper mapper, 
            ILogger logger, 
            IRepository<OrderItem> repository, 
            IUnitOfWork unitOfWork,
            IRepository<ProductAttribute> productAttributeRepository
        )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _productAttributeRepository = productAttributeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<OrderItemDTO>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var orderItem = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (orderItem is not null)
                    {
                        await _repository.Remove(orderItem);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        var att = _productAttributeRepository.GetAll().FirstOrDefault(x => x.Id == orderItem.ProductAttributeId);
                        if (att != null)
                        {
                            att.Amount += (int)orderItem.CountBought;
                            _productAttributeRepository.Update(att, att);
                        }
                        return new ResponseResultAPI<OrderItemDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<OrderItemDTO>(orderItem),
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
