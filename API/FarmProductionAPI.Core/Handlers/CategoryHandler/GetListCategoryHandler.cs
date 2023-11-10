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
    public class GetListCategoryHandler : IRequestHandler<GetListCategoryQuery, ResponseResultAPI<List<CategoryDTO>>>
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Category> _repository;

        public GetListCategoryHandler(IMapper mapper, ILogger logger, IRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseResultAPI<List<CategoryDTO>>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = _repository.GetAll().AsQueryable().Where(x => 
                    request == null || (string.IsNullOrEmpty(request.Code) || x.Code.ToLower().Contains(request.Code)) &&
                    (string.IsNullOrEmpty(request.Name) || x.Name.ToLower().Contains(request.Name)));
                //var parentCategories = _mapper.Map<List<ParentCategoryDTO>>(categories);
                //if (parentCategories.Any())
                //{
                //    foreach (var item in parentCategories)
                //    {
                //        var listSubCategories = _repository.GetAll().AsQueryable().Where(x => x.ParentCategoryId != null && x.ParentCategoryId == item.Id && request == null || (string.IsNullOrEmpty(request.Code) || x.Code.ToLower().Contains(request.Code)) &&
                //            (string.IsNullOrEmpty(request.Name) || x.Name.ToLower().Contains(request.Name)));

                //        if (listSubCategories.Any())
                //        {
                //            item.SubCategories = _mapper.Map<List<CategoryDTO>>(listSubCategories);
                //        }
                //    }
                //}

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
