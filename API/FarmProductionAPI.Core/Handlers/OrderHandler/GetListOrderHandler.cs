using AutoMapper;
using FarmProductionAPI.Core.Queries.OrderQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.OrderHandler
{
    public class GetListOrderHandler : IRequestHandler<GetListOrderQuery, ResponseResultAPI<List<OrderDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Order> _repository;

        public GetListOrderHandler(IMapper mapper, ILogger logger, IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<OrderDTO>>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = _repository.GetAll().ToList();
                return new ResponseResultAPI<List<OrderDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<OrderDTO>>(categories),
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
