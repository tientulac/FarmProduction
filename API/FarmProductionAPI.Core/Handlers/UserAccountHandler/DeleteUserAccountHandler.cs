using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.UserAccountCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.UserAccountHandler
{
    public class DeleteUserAccountHandler : ICommandHandler<DeleteUserAccountCommand, ResponseResultAPI<UserAccountDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<UserAccount> _repository;

        public DeleteUserAccountHandler(IMapper mapper, ILogger logger, IRepository<UserAccount> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<UserAccountDTO>> Handle(DeleteUserAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var UserAccount = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (UserAccount is not null)
                    {
                        await _repository.Remove(UserAccount);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<UserAccountDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<UserAccountDTO>(UserAccount),
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

                return new ResponseResultAPI<UserAccountDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
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
