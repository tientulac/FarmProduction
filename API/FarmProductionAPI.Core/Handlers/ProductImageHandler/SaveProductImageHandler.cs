using AutoMapper;
using FarmProductionAPI.Core.Commands.ProductImageCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductImageHandler
{
    public class SaveProductImageHandler : ICommandHandler<SaveProductImageCommand, ResponseResultAPI<ProductImageDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductImage> _repository;

        public SaveProductImageHandler(IMapper mapper, ILogger logger, IRepository<ProductImage> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProductImageDTO>> Handle(SaveProductImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productImage = new ProductImage();
                if (request.Id.HasValue)
                {
                    productImage = await _repository.GetById(request.Id.Value);
                    if (productImage is not null)
                    {
                        await _repository.Update(_mapper.Map<ProductImage>(request), productImage);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        return new ResponseResultAPI<ProductImageDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                productImage = _mapper.Map<ProductImage>(request);
                await _repository.CreateOneAsync(productImage, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<ProductImageDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<ProductImageDTO>(productImage),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<ProductImageDTO>()
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
