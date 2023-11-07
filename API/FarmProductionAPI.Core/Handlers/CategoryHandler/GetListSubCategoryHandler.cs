using AutoMapper;
using FarmProductionAPI.Core.Queries.CategoryQuery;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Core.Handlers.CategoryHandler
{
    public class GetListSubCategoryHandler : IRequestHandler<GetListSubCategoryQuery, ResponseResultAPI<List<CategoryDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Category> _repository;

        public GetListSubCategoryHandler(IMapper mapper, ILogger logger, IRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseResultAPI<List<CategoryDTO>>> Handle(GetListSubCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = _repository.GetAll().Where(x => x.ParentCategoryId != null).AsQueryable().OrderBy(x => x.Name);

                return new ResponseResultAPI<List<CategoryDTO>>()
                {
                    Code = "200",
                    Data = _mapper.Map<List<CategoryDTO>>(categories),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<CategoryDTO>>()
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
