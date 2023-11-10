using AutoMapper;
using FarmProductionAPI.Core.Queries.RoleQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.RoleHandler
{
    public class GetListRoleHandler : IRequestHandler<GetListRoleQuery, ResponseResultAPI<List<RoleDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Role> _repository;

        public GetListRoleHandler(IMapper mapper, ILogger logger, IRepository<Role> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<RoleDTO>>> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = _repository.GetAll().ToList();
                return new ResponseResultAPI<List<RoleDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<RoleDTO>>(roles),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<RoleDTO>>()
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
