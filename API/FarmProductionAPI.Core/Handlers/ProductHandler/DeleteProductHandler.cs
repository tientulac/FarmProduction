using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.ProductCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.ProductHandler
{
    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand, ResponseResultAPI<ProductDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Product> _repository;

        public DeleteProductHandler(IMapper mapper, ILogger logger, IRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProductDTO>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var Product = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (Product is not null)
                    {
                        await _repository.Remove(Product);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<ProductDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<ProductDTO>(Product),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<ProductDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<ProductDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<ProductDTO>()
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
