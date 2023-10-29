using AutoMapper;
using FarmProductionAPI.Core.Commands.CategoryCommand;
using FarmProductionAPI.Core.Commands.ProductCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductHandler
{
    public class SaveProductHandler : ICommandHandler<SaveProductCommand, ResponseResultAPI<ProductDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Product> _repository;

        public SaveProductHandler(IMapper mapper, ILogger logger, IRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProductDTO>> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product();
                if (request.Id.HasValue)
                {
                    product = await _repository.GetById(request.Id.Value);
                    if (product is not null)
                    {
                        await _repository.Update(_mapper.Map<Product>(request), product);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
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

                product = _mapper.Map<Product>(request);
                await _repository.CreateOneAsync(product, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<ProductDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<ProductDTO>(product),
                    Message = "Success"
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
