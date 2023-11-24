using AutoMapper;
using FarmProductionAPI.Core.Commands.UserAccountCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.UserAccountHandler
{
    public class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand, ResponseResultAPI<UserAccountDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<UserAccount> _repository;

        public ChangePasswordHandler(IMapper mapper, ILogger logger, IRepository<UserAccount> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<UserAccountDTO>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new UserAccount();
                if (request.Id.HasValue && !string.IsNullOrEmpty(request.NewHashpassword))
                {
                    user = await _repository.GetFirstByConditionAsync(x => x.Id == request.Id);
                    if (user is null)
                    {
                        return new ResponseResultAPI<UserAccountDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                    user.Hashpassword = request.NewHashpassword;
                    await _repository.Update(user, user);
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
