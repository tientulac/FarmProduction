using AutoMapper;
using FarmProductionAPI.Core.Queries.ProducerQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProducerHandler
{
    public class GetListProducerHandler : IRequestHandler<GetListProducerQuery, ResponseResultAPI<List<ProducerDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Producer> _repository;

        public GetListProducerHandler(IMapper mapper, ILogger logger, IRepository<Producer> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<ProducerDTO>>> Handle(GetListProducerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var producers = _repository.GetAll().AsQueryable().Where(x =>
                    request == null || (string.IsNullOrEmpty(request.Code) || x.Code.ToLower().Contains(request.Code)) &&
                    (string.IsNullOrEmpty(request.Name) || x.Name.ToLower().Contains(request.Name)));

                return new ResponseResultAPI<List<ProducerDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<ProducerDTO>>(producers),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<ProducerDTO>>()
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
