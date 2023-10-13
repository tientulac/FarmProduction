﻿using AutoMapper;
using FarmProductionAPI.Core.Commands.UserAccountCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.UserAccountHandler
{
    public class SaveUserAccountHandler : ICommandHandler<SaveUserAccountCommand, ResponseResultAPI<UserAccountDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<UserAccount> _repository;

        public SaveUserAccountHandler(IMapper mapper, ILogger logger, IRepository<UserAccount> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<UserAccountDTO>> Handle(SaveUserAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var UserAccount = new UserAccount();
                if (request.Id.HasValue)
                {
                    UserAccount = await _repository.GetById(request.Id.Value);
                    if (UserAccount is not null)
                    {
                        await _repository.Update(_mapper.Map<UserAccount>(request));
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        return new ResponseResultAPI<UserAccountDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                UserAccount = _mapper.Map<UserAccount>(request);
                await _repository.CreateOneAsync(UserAccount, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<UserAccountDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<UserAccountDTO>(UserAccount),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<UserAccountDTO>()
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