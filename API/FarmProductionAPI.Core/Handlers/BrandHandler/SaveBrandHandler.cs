using AutoMapper;
using FarmProductionAPI.Core.Commands.BrandCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.BrandHandler
{
    public class SaveBrandHandler : ICommandHandler<SaveBrandCommand, ResponseResultAPI<BrandDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Brand> _repository;

        public SaveBrandHandler(IMapper mapper, ILogger logger, IRepository<Brand> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<BrandDTO>> Handle(SaveBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var brand = new Brand();
                if (request.Id.HasValue)
                {
                    brand = await _repository.GetById(request.Id.Value);
                    if (brand is not null)
                    {
                        await _repository.Update(_mapper.Map<Brand>(request), brand);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
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

                brand = _mapper.Map<Brand>(request);
                await _repository.CreateOneAsync(brand, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<BrandDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<BrandDTO>(brand),
                    Message = "Success"
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
