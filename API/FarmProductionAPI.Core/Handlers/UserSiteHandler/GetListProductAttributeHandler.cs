using AutoMapper;
using FarmProductionAPI.Core.Queries.ProductAttributeQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.UserSiteHandler
{
    public class GetListProductAttributeHandler : IRequestHandler<GetListProductAttributeQuery, ResponseResultAPI<List<ProductAttributeDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductAttribute> _repository;

        public GetListProductAttributeHandler(IMapper mapper, ILogger logger, IRepository<ProductAttribute> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<ProductAttributeDTO>>> Handle(GetListProductAttributeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var atts = _repository.GetAllUserSite().Include(x => x.Product).ThenInclude(x => x.ProductImages).AsQueryable();
                if (request.ProductId.HasValue)
                {
                    atts = atts.Where(x => x.ProductId == request.ProductId);
                }
                if (!string.IsNullOrEmpty(request.Color))
                {
                    atts = atts.Where(x => x.Color == request.Color);
                }
                if (!string.IsNullOrEmpty(request.Unit))
                {
                    atts = atts.Where(x => x.Unit == request.Unit);
                    if (!string.IsNullOrEmpty(request.Value))
                    {
                        atts = atts.Where(x => x.Value == request.Value);
                    }
                }
                if (request.FilterType != null)
                {
                    switch (request.FilterType)
                    {
                        case FilterPriceType.GREATER:
                            atts = atts.Where(x => x.Price > request.Price);
                            break;
                        case FilterPriceType.EQUAL:
                            atts = atts.Where(x => x.Price == request.Price);
                            break;
                        case FilterPriceType.LESS:
                            atts = atts.Where(x => x.Price < request.Price);
                            break;
                        default:
                            break;
                    }
                }

                return new ResponseResultAPI<List<ProductAttributeDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<ProductAttributeDTO>>(atts),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<ProductAttributeDTO>>()
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
