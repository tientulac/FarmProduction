﻿using AutoMapper;
using FarmProductionAPI.Core.Commands.RoleCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.RoleHandler
{
    public class SaveRoleHandler : ICommandHandler<SaveRoleCommand, ResponseResultAPI<RoleDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Role> _repository;

        public SaveRoleHandler(IMapper mapper, ILogger logger, IRepository<Role> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<RoleDTO>> Handle(SaveRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Role = new Role();
                if (request.Id.HasValue)
                {
                    Role = await _repository.GetById(request.Id.Value);
                    if (Role is not null)
                    {
                        await _repository.Update(_mapper.Map<Role>(request));
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
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

                Role = _mapper.Map<Role>(request);
                await _repository.CreateOneAsync(Role, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<RoleDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<RoleDTO>(Role),
                    Message = "Success"
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