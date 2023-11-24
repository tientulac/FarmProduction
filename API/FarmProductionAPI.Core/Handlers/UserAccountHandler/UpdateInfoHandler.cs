using AutoMapper;
using FarmProductionAPI.Core.Commands.UserAccountCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.UserAccountHandler
{
    public class UpdateInfoHandler : ICommandHandler<UpdateInfoCommand, ResponseResultAPI<UserAccountDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<UserAccount> _repository;

        public UpdateInfoHandler(IMapper mapper, ILogger logger, IRepository<UserAccount> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<UserAccountDTO>> Handle(UpdateInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userAccount = await _repository.GetById(request.Id.Value);
                if (userAccount is not null)
                {
                    userAccount.Email = request.Email;
                    userAccount.Phone = request.Phone;
                    userAccount.FullName = request.FullName;
                    userAccount.Address = request.Address;
                    await _repository.Update(userAccount, userAccount);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    return new ResponseResultAPI<UserAccountDTO>()
                    {
                        Code = "200",
                        Data = _mapper.Map<UserAccountDTO>(userAccount),
                        Message = "Success"
                    };
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
