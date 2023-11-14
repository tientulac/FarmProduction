using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.ProducerCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.ProducerHandler
{
    public class DeleteProducerHandler : ICommandHandler<DeleteProducerCommand, ResponseResultAPI<ProducerDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Producer> _repository;

        public DeleteProducerHandler(IMapper mapper, ILogger logger, IRepository<Producer> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProducerDTO>> Handle(DeleteProducerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var producer = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (producer is not null)
                    {
                        await _repository.Remove(producer);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<ProducerDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<ProducerDTO>(producer),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<ProducerDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<ProducerDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<ProducerDTO>()
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
