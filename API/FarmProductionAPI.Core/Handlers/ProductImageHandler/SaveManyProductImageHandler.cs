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
    public class SaveManyProductImageHandler : ICommandHandler<SaveManyProductImageCommand, ResponseResultAPI<List<ProductImageDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductImage> _repository;

        public SaveManyProductImageHandler(IMapper mapper, ILogger logger, IRepository<ProductImage> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<ProductImageDTO>>> Handle(SaveManyProductImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Images.Any())
                {
                    //var productImages = _repository.GetAll().Where(x => x.ProductId == request.ProductId).ToList();
                    //await _repository.RemoveManyAsync(productImages, cancellationToken);

                    foreach (var item in request.Images)
                    {
                        var productImage = new ProductImage() { 
                            ProductId = request.ProductId,
                            Image = item
                        };
                        await _repository.CreateOneAsync(productImage, cancellationToken);
                    }
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                return new ResponseResultAPI<List<ProductImageDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<ProductImageDTO>>(_repository.GetAll().AsQueryable().Where(x => x.ProductId == request.ProductId)),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<ProductImageDTO>>()
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
