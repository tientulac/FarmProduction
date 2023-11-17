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
    public class ExportProductHandler : ICommandHandler<ExportExcelCommand<ProductExport>, ResponseResultAPI<byte[]>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<ProductAttribute> _repository;
        private readonly IExportExcel<ProductExport> _exportService;

        public ExportProductHandler(
            IMapper mapper,
            ILogger logger,
            IRepository<ProductAttribute> repository,
            IUnitOfWork unitOfWork,
            IExportExcel<ProductExport> exportService)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _exportService = exportService;
        }

        public async Task<ResponseResultAPI<byte[]>> Handle(ExportExcelCommand<ProductExport> request, CancellationToken cancellationToken)
        {
            try
            {
                var list = _repository.GetAll().Include(p => p.Product).AsQueryable().Select(x => new ProductExport
                {
                   ProductName = x.Product.Name,
                   Code = x.Code,
                   Price = x.Price,
                   Amount = x.Amount,
                   Unit = x.Unit,
                   Value = x.Value,
                   ManufactureDate = x.ManufactureDate == null ? "" : x.ManufactureDate.GetValueOrDefault().ToString("dd/MM/yyyy"),
                   ExpireDate = x.ExpireDate == null ? "" : x.ExpireDate.GetValueOrDefault().ToString("dd/MM/yyyy"),
                }).ToList();

                var rs = _exportService.ExportToExcel(list, "ProductAttribute");

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
