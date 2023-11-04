using AutoMapper;
using FarmProductionAPI.Core.Queries.BrandQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.BrandHandler
{
    public class GetListBrandHandler : IRequestHandler<GetListBrandQuery, ResponseResultAPI<List<BrandDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Brand> _repository;

        public GetListBrandHandler(IMapper mapper, ILogger logger, IRepository<Brand> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<BrandDTO>>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var brands = _repository.GetAll().AsQueryable().Where(x =>
                    request == null || (string.IsNullOrEmpty(request.Code) || x.Code.ToLower().Contains(request.Code)) &&
                    (string.IsNullOrEmpty(request.Name) || x.Name.ToLower().Contains(request.Name)));

                return new ResponseResultAPI<List<BrandDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<BrandDTO>>(brands),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<BrandDTO>>()
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
