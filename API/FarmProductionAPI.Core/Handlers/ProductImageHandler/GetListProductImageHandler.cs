using AutoMapper;
using FarmProductionAPI.Core.Queries.ProductImageQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductImageHandler
{
    public class GetListProductImageHandler : IRequestHandler<GetListProductImageQuery, ResponseResultAPI<List<ProductImageDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductImage> _repository;

        public GetListProductImageHandler(IMapper mapper, ILogger logger, IRepository<ProductImage> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<ProductImageDTO>>> Handle(GetListProductImageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pImages = _repository.GetAll().AsQueryable();

                if (request.ProductId.HasValue)
                {
                    pImages = pImages.Where(x => x.ProductId == request.ProductId && x.IsSoftDeleted != true);
                }

                return new ResponseResultAPI<List<ProductImageDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<ProductImageDTO>>(pImages),
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
