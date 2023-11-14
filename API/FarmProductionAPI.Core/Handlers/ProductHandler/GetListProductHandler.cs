using AutoMapper;
using FarmProductionAPI.Core.Queries.ProductQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductHandler
{
    public class GetListProductHandler : IRequestHandler<GetListProductQuery, ResponseResultAPI<List<ProductDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Product> _repository;

        public GetListProductHandler(IMapper mapper, ILogger logger, IRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<ProductDTO>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = _repository.GetAll().AsQueryable().Where(x =>
                    request == null || (string.IsNullOrEmpty(request.Code) || x.Code.ToLower().Contains(request.Code)) &&
                    (string.IsNullOrEmpty(request.Name) || x.Name.ToLower().Contains(request.Name)) &&
                    (request.BrandId == null || x.BrandId == request.BrandId) &&
                    (request.CategoryId == null || x.CategoryId == request.CategoryId))
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Include(x => x.Producer)
                    .Include(x =>  x.ProductDescriptions.Where(x => x.IsSoftDeleted != true));

                return new ResponseResultAPI<List<ProductDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<ProductDTO>>(products),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<ProductDTO>>()
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
