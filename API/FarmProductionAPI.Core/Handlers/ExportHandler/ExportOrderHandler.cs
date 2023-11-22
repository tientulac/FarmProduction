using AutoMapper;
using FarmProductionAPI.Core;
using FarmProductionAPI.Core.Commands.ExportCommand;
using FarmProductionAPI.Core.Handlers;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.ExportModels;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmOrderionAPI.Core.Handlers.ExportHandler
{
    public class ExportOrderHandler : ICommandHandler<ExportExcelCommand<OrderExport>, ResponseResultAPI<byte[]>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Order> _repository;
        private readonly IExportExcel<OrderExport> _exportService;

        public ExportOrderHandler(
            IMapper mapper,
            ILogger logger,
            IRepository<Order> repository,
            IUnitOfWork unitOfWork,
            IExportExcel<OrderExport> exportService)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _exportService = exportService;
        }

        public async Task<ResponseResultAPI<byte[]>> Handle(ExportExcelCommand<OrderExport> request, CancellationToken cancellationToken)
        {
            try
            {
                var list = _repository.GetAll().Include(p => p.OrderItems).AsQueryable().Select(x => new OrderExport
                {
                    Code = x.Code,
                    Status = x.Status.ToString(),
                    Type = x.Type.ToString(),
                    UserAccount = x.UserAccount != null ? x.UserAccount.FullName : "",
                    SellerAccount = x.SellerAccountId.ToString() ?? "",
                    Total = x.OrderItems != null ? x.OrderItems.Where(x => x.IsSoftDeleted != true).Sum(x => x.UnitPrice * x.CountBought) : 0,
                    PaymentShip = x.PaymentShip,
                    PaymentType = x.PaymentType.ToString(),
                    CreatedAt = x.CreatedAt == null ? "" : x.CreatedAt.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    UpdatedAt = x.UpdatedAt == null ? "" : x.UpdatedAt.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    DeletedAt = x.DeletedAt == null ? "" : x.DeletedAt.GetValueOrDefault().ToString("dd/MM/yyyy"),
                    IsSoftDeleted = x.IsSoftDeleted
                }).ToList();

                var rs = _exportService.ExportToExcel(list, "Order");

                return new ResponseResultAPI<byte[]>()
                {
                    Code = "200",
                    Data = rs,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
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
