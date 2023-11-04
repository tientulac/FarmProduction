using AutoMapper;
using FarmProductionAPI.Core.Commands.ProductDescriptionCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductDescriptionHandler
{
    public class SaveProductDescriptionHandler : ICommandHandler<SaveProductDescriptionCommand, ResponseResultAPI<ProductDescriptionDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductDescription> _repository;

        public SaveProductDescriptionHandler(IMapper mapper, ILogger logger, IRepository<ProductDescription> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProductDescriptionDTO>> Handle(SaveProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productDescription = new ProductDescription();
                productDescription = _mapper.Map<ProductDescription>(request);
                await _repository.CreateOneAsync(productDescription, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<ProductDescriptionDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<ProductDescriptionDTO>(productDescription),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<ProductDescriptionDTO>()
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
