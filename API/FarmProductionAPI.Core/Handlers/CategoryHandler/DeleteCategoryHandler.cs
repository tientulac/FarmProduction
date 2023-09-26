using AutoMapper;
using Azure.Core;
using DynamicExpressions.Mapping;
using FarmProductionAPI.Core.Commands.CategoryCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using Serilog;
using System.Threading;

namespace FarmProductionAPI.Core.Handlers.CategoryHandler
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategoryCommand, ResponseResultAPI<CategoryDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Category> _repository;

        public DeleteCategoryHandler(IMapper mapper, ILogger logger, IRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<CategoryDTO>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var category = await _repository.GetByIdAsync(request.Id.Value, db => db, cancellationToken);
                    if (category is not null)
                    {
                        await _repository.Remove(category);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return new ResponseResultAPI<CategoryDTO>()
                        {
                            Code = "200",
                            Data = _mapper.Map<CategoryDTO>(category),
                            Message = "Success"
                        };
                    }
                    else
                    {
                        return new ResponseResultAPI<CategoryDTO>()
                        {
                            Code = "404",
                            Data = null,
                            Message = "Not found"
                        };
                    }
                }

                return new ResponseResultAPI<CategoryDTO>()
                {
                    Code = "404",
                    Data = null,
                    Message = "Not found"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<CategoryDTO>()
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
