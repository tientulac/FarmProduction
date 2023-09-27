using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.ProductImageCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.ProductImageHandler
{
    public class DeleteProductImageHandler : ICommandHandler<DeleteProductImageCommand, ResponseResultAPI<ProductImageDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductImage> _repository;

        public DeleteProductImageHandler(IMapper mapper, ILogger logger, IRepository<ProductImage> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProductImageDTO>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var ProductImage = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (ProductImage is not null)
                    {
                        await _repository.Remove(ProductImage);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<ProductImageDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<ProductImageDTO>(ProductImage),
                            Message = "Success"
                        };
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

                return new ResponseResultAPI<ProductImageDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
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
