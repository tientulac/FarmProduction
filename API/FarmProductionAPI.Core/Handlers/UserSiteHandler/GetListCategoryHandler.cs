
using AutoMapper;
using FarmProductionAPI.Core.Queries.UserSite;
using FarmProductionAPI.Core.Repositories;
using FarmProductionAPI.Domain.Dtos;
using FarmProductionAPI.Domain.Models;
using FarmProductionAPI.Domain.Response;
using MediatR;
using Serilog;
using System.Collections.Generic;

namespace FarmProductionAPI.Core.Handlers.UserSiteHandler
{
    public class GetListCategoryHandler : IRequestHandler<GetListCategoryQuery, ResponseResultAPI<List<ParentCategoryDTO>>>
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
        public async Task<ResponseResultAPI<List<ParentCategoryDTO>>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var parentCategories = _repository.GetAllUserSite().AsQueryable().Where(x => x.ParentCategoryId == null);
                var list = new List<ParentCategoryDTO>();

                if (parentCategories.Any())
                {
                    foreach (var parentCategory in parentCategories)
                    {
                        var categories = _repository.GetAllUserSite().AsQueryable().Where(x => x.ParentCategoryId == parentCategory.Id);
                        if (categories.Any())
                        {
                            var rs = _mapper.Map<ParentCategoryDTO>(parentCategory);
                            rs.SubCategories = new List<CategoryDTO>();
                            rs.SubCategories.AddRange(_mapper.Map<List<CategoryDTO>>(categories));
                            list.Add(rs);
                        }
                    }
                }


                return new ResponseResultAPI<List<ParentCategoryDTO>>()
                {
                    Code = "200",
                    Data = list,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResultAPI<List<ParentCategoryDTO>>()
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
