using AutoMapper;
using FarmProductionAPI.Core.Queries.ProductDescriptionQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductDescriptionHandler
{
    public class GetListProductDescriptionHandler : IRequestHandler<GetListProductDescriptionQuery, ResponseResultAPI<List<ProductDescriptionDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductDescription> _repository;

        public GetListProductDescriptionHandler(IMapper mapper, ILogger logger, IRepository<ProductDescription> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<ProductDescriptionDTO>>> Handle(GetListProductDescriptionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pDescriptions = _repository.GetAll().AsQueryable().Where(x => 
                    string.IsNullOrEmpty(request.SearchStringKeyword) || x.Description.ToLower().Contains(request.SearchStringKeyword));
                
                if (request.ProductId.HasValue)
                {
                    pDescriptions = pDescriptions.Where(x => x.ProductId == request.ProductId);
                }
                return new ResponseResultAPI<List<ProductDescriptionDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<ProductDescriptionDTO>>(pDescriptions),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<ProductDescriptionDTO>>()
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
