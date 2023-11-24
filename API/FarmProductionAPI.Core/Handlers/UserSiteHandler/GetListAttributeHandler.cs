
using AutoMapper;
using FarmProductionAPI.Core.Queries.UserSiteQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.UserSiteHandler
{
    public class GetListAttributeHandler : IRequestHandler<GetListAttributeQuery, ResponseResultAPI<AttributeDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductAttribute> _repository;
        private readonly IRepository<ProductDescription> _descriptionRepository;

        public GetListAttributeHandler(IMapper mapper, ILogger logger, IRepository<ProductAttribute> repository, IRepository<ProductDescription> descriptionRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _descriptionRepository = descriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<AttributeDTO>> Handle(GetListAttributeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var atts = _repository.GetAllUserSite().AsQueryable().Where(x => request.ProductId == null || x.ProductId == request.ProductId);
                var descs = _descriptionRepository.GetAllUserSite().AsQueryable().Where(x => request.ProductId == null || x.ProductId == request.ProductId);

                var rs = new AttributeDTO();
                rs.Colors = atts.Select(x => x.Color).Distinct().ToList();
                rs.Descriptions = descs.Select(x => x.Description).Distinct().ToList();


                return new ResponseResultAPI<AttributeDTO>()
                {
                    Code = "200",
                    Data = rs,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<AttributeDTO>()
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
