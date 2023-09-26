using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.BrandHandler
{
    public class DeleteBrandHandler : ICommandHandler<DeleteBrandCommand, ResponseResultAPI<BrandDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Brand> _repository;

        public DeleteBrandHandler(IMapper mapper, ILogger logger, IRepository<Brand> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<BrandDTO>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var brand = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (brand is not null)
                    {
                        await _repository.Remove(brand);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<BrandDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<BrandDTO>(brand),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<BrandDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<BrandDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<BrandDTO>()
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
