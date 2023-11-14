using AutoMapper;
using FarmProductionAPI.Core.Queries.UserAccountQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.UserAccountHandler
{
    public class GetListUserAccountHandler : IRequestHandler<GetListUserAccountQuery, ResponseResultAPI<List<UserAccountDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<UserAccount> _repository;

        public GetListUserAccountHandler(IMapper mapper, ILogger logger, IRepository<UserAccount> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<List<UserAccountDTO>>> Handle(GetListUserAccountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = _repository.GetAll().AsQueryable().Where(x =>
                    (string.IsNullOrEmpty(request.UserName) || x.UserName.Contains(request.UserName)) &&
                    (string.IsNullOrEmpty(request.Email) || x.Email.Contains(request.Email)) &&
                    (string.IsNullOrEmpty(request.Phone) || x.Phone.Contains(request.Phone)) &&
                    (string.IsNullOrEmpty(request.FullName) || x.FullName.Contains(request.FullName)) &&
                    (request.RoleId == null || x.RoleId == request.RoleId) 
                    ).Include(x => x.Role);
                return new ResponseResultAPI<List<UserAccountDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<UserAccountDTO>>(categories),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<UserAccountDTO>>()
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
