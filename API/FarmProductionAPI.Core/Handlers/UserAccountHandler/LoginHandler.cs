using AutoMapper;
using FarmProductionAPI.Core.Commands.CategoryCommand;
using FarmProductionAPI.Core.Commands.UserAccountCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductionAPI.Core.Handlers.UserAccountHandler
{
    public class LoginHandler : ICommandHandler<LoginCommand, ResponseResultAPI<UserAccountDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<UserAccount> _repository;

        public LoginHandler(IMapper mapper, ILogger logger, IRepository<UserAccount> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<UserAccountDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new UserAccount();
                if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Hashpassword))
                {
                    user = await _repository.GetFirstByConditionAsync(x => x.UserName == request.UserName && x.Hashpassword == request.Hashpassword, db => db.Include(x => x.Role));
                    if (user is null)
                    {
                        return new ResponseResultAPI<UserAccountDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<UserAccountDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<UserAccountDTO>(user),
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
