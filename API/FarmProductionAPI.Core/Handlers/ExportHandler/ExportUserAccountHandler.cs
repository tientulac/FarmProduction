using AutoMapper;
using FarmProductionAPI.Core.Commands.ExportCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.ExportModels;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ExportHandler
{
    public class ExportUserAccountHandler : ICommandHandler<ExportExcelCommand<UserAccountExport>, ResponseResultAPI<byte[]>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<UserAccount> _repository;
        private readonly IExportExcel<UserAccountExport> _exportService;

        public ExportUserAccountHandler(
            IMapper mapper,
            ILogger logger,
            IRepository<UserAccount> repository,
            IUnitOfWork unitOfWork,
            IExportExcel<UserAccountExport> exportService)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _exportService = exportService;
        }

        public async Task<ResponseResultAPI<byte[]>> Handle(ExportExcelCommand<UserAccountExport> request, CancellationToken cancellationToken)
        {
            try
            {
                var list = _repository.GetAll().Include(u => u.Role).AsQueryable().Select(x => new UserAccountExport
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    Phone = x.Phone,
                    FullName = x.FullName,
                    Address = x.Address,
                    RoleName = x.Role != null ? x.Role.Name : "",
                    CreatedAt = x.CreatedAt.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    IsSoftDeleted = x.IsSoftDeleted,
                }).ToList();

                var rs = _exportService.ExportToExcel(list, "UserAccount");

                return new ResponseResultAPI<byte[]>()
                {
                    Code = "200",
                    Data = rs,
                    Message = "Success"
                };
            }
            catch (Exception ex) {
                return new ResponseResultAPI<byte[]>()
                {
                    Code = "500",
                    Data = null,
                    MessageEX = ex.Message
                };
            }
        }
    }
}
