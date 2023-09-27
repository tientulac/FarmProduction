using AutoMapper;
using FarmProductionAPI.Core.Queries.ProductAttributeQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProductAttributeHandler
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
                var atts = _repository.GetAll().ToList();
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
