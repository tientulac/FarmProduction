using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.RoleCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.RoleHandler
{
    public class DeleteRoleHandler : ICommandHandler<DeleteRoleCommand, ResponseResultAPI<RoleDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Role> _repository;

        public DeleteRoleHandler(IMapper mapper, ILogger logger, IRepository<Role> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<RoleDTO>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var Role = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (Role is not null)
                    {
                        await _repository.Remove(Role);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<RoleDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<RoleDTO>(Role),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<RoleDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<RoleDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<RoleDTO>()
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
