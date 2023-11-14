using AutoMapper;
using FarmProductionAPI.Core.Commands.ProducerCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.ProducerHandler
{
    public class SaveProducerHandler : ICommandHandler<SaveProducerCommand, ResponseResultAPI<ProducerDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Producer> _repository;

        public SaveProducerHandler(IMapper mapper, ILogger logger, IRepository<Producer> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<ProducerDTO>> Handle(SaveProducerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var producer = new Producer();
                if (request.Id.HasValue)
                {
                    producer = await _repository.GetById(request.Id.Value);
                    if (producer is not null)
                    {
                        await _repository.Update(_mapper.Map<Producer>(request), producer);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
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

                producer = _mapper.Map<Producer>(request);
                await _repository.CreateOneAsync(producer, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<ProducerDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<ProducerDTO>(producer),
                    Message = "Success"
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
