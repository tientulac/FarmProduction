using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.ProductDescriptionCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.ProductDescriptionHandler
{
    public class DeleteProductDescriptionHandler : ICommandHandler<DeleteProductDescriptionCommand, ResponseResultAPI<ProductDescriptionDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductDescription> _repository;

        public DeleteProductDescriptionHandler(IMapper mapper, ILogger logger, IRepository<ProductDescription> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProductDescriptionDTO>> Handle(DeleteProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var productDescription = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (productDescription is not null)
                    {
                        await _repository.Remove(productDescription);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<ProductDescriptionDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<ProductDescriptionDTO>(productDescription),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<ProductDescriptionDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<ProductDescriptionDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
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
