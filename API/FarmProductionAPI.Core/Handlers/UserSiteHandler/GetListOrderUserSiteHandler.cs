using AutoMapper;
using FarmProductionAPI.Core.Queries.OrderQuery;
using FarmProductionAPI.Core.Queries.UserSiteQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.OrderHandler
{
    public class GetListOrderUserSiteHandler : IRequestHandler<GetListOrderUserSiteQuery, ResponseResultAPI<List<OrderDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Order> _repository;

        public GetListOrderUserSiteHandler(IMapper mapper, ILogger logger, IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<OrderDTO>>> Handle(GetListOrderUserSiteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = _repository.GetAllUserSite().Include(x => x.UserAccount).Include(x => x.OrderItems).AsQueryable();
                if (!string.IsNullOrEmpty(request.ProvinceToId))
                {
                    orders = orders.Where(x => x.ProvinceToId == request.ProvinceToId);
                }
                if (!string.IsNullOrEmpty(request.DistrictToId))
                {
                    orders = orders.Where(x => x.DistrictToId == request.DistrictToId);
                }
                if (!string.IsNullOrEmpty(request.WardToId))
                {
                    orders = orders.Where(x => x.WardToId == request.WardToId);
                }
                if (!string.IsNullOrEmpty(request.Code))
                {
                    orders = orders.Where(x => x.Code.Contains(request.Code));
                }
                if (request.Status != null)
                {
                    orders = orders.Where(x => x.Status == request.Status);
                }
                if (request.Type != null)
                {
                    orders = orders.Where(x => x.Type == request.Type);
                }
                if (!string.IsNullOrEmpty(request.UserName))
                {
                    orders = orders.Where(x => x.UserAccount != null && x.UserAccount.UserName.Contains(request.UserName));
                }
                if (request.PaymentType != null)
                {
                    orders = orders.Where(x => x.PaymentType == request.PaymentType);
                }
                if (request.FromDate != null)
                {
                    orders = orders.Where(x => x.CreatedAt >= request.FromDate);
                }
                if (request.ToDate != null)
                {
                    orders = orders.Where(x => x.CreatedAt <= request.ToDate);
                }
                if (request.UserAccountId.HasValue)
                {
                    orders = orders.Where(x => x.UserAccountId == request.UserAccountId);
                }
                return new ResponseResultAPI<List<OrderDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<OrderDTO>>(orders),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<OrderDTO>>()
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
