using AutoMapper;
using FarmProductionAPI.Core.Commands.CategoryCommand;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using OneOf;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.CategoryHandler
{
    public class SaveCategoryHandler : ICommandHandler<SaveCategoryCommand, ResponseResultAPI<CategoryDTO>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Category> _repository;

        public SaveCategoryHandler(IMapper mapper, ILogger logger, IRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseResultAPI<CategoryDTO>> Handle(SaveCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category();
                if (request.Id.HasValue)
                {
                    category = await _repository.GetById(request.Id.Value);
                    if (category is not null)
                    {
                        await _repository.Update(_mapper.Map<Category>(request), category);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
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

                category = _mapper.Map<Category>(request);
                await _repository.CreateOneAsync(category, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new ResponseResultAPI<CategoryDTO>()
                {
                    Code = "200",
                    Data = _mapper.Map<CategoryDTO>(category),
                    Message = "Success"
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
