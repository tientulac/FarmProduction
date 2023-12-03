

using AutoMapper;
using FarmProductionAPI.Core.Queries.UserSiteQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.UserSiteHandler
{
    public class GetListProductHandler : IRequestHandler<GetListProductUserSiteQuery, ResponseResultAPI<List<ProductDTO>>>
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

        public async Task<ResponseResultAPI<List<ProductDTO>>> Handle(GetListProductUserSiteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = _repository.GetAllUserSite().AsQueryable().Where(x =>
                    request == null || (string.IsNullOrEmpty(request.SearchString) || x.Code.ToLower().Contains(request.SearchString.ToLower())) &&
                    (string.IsNullOrEmpty(request.SearchString) || x.Name.ToLower().Contains(request.SearchString.ToLower())) &&
                    (request.BrandId == null || x.BrandId == request.BrandId) &&
                    (request.CategoryId == null || x.CategoryId == request.CategoryId))
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Include(x => x.Producer)
                    .Include(x => x.ProductDescriptions.Where(x => x.IsSoftDeleted != true));

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
