using AutoMapper;
using FarmProductionAPI.Core.Commands.ProductAttributeCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductAttributeHandler
{
    public class DeleteProductAttributeHandler : ICommandHandler<DeleteProductAttributeCommand, ResponseResultAPI<ProductAttributeDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductAttribute> _repository;

        public DeleteProductAttributeHandler(IMapper mapper, ILogger logger, IRepository<ProductAttribute> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProductAttributeDTO>> Handle(DeleteProductAttributeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var ProductAttribute = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (ProductAttribute is not null)
                    {
                        await _repository.Remove(ProductAttribute);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<ProductAttributeDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<ProductAttributeDTO>(ProductAttribute),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<ProductAttributeDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<ProductAttributeDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<ProductAttributeDTO>()
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
