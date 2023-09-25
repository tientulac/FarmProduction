using FarmProductionAPI.Core.PagingHelper;
using FarmProductionAPI.Domain;
using FarmProductionAPI.Domain.Models;
using Microsoft.AspNetCore.WebUtilities;

using Microsoft.EntityFrameworkCore;

namespace FarmProductionAPI.Core.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {

        protected readonly DataContext _dbContext;
        protected IQueryable<TEntity> _queryAble;

        public BaseRepository(DataContext dataContext)
        {
            _dbContext = dataContext;
            _queryAble = dataContext.Set<TEntity>()
                .AsQueryable();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _queryAble.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _queryAble.AsQueryable();
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var insertedItem = _dbContext.Set<TEntity>()
                .Add(entity)
                .Entity;

            await _dbContext.SaveChangesAsync();
            return insertedItem;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var updatedItem = _dbContext.Set<TEntity>()
                .Update(entity).Entity;

            await _dbContext.SaveChangesAsync();
            return updatedItem;
        }

        public virtual async Task Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>()
                .Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<PaginatedList<TEntity>> GetAll(PagingAndSortingModel pagingAndSortingModel)
        {
            var itemsQuerys = QueryHelper.BuildOrderExpression(_queryAble, pagingAndSortingModel);
            return await PaginatedList<TEntity>.CreateAsync(itemsQuerys, pagingAndSortingModel.PageIndex, pagingAndSortingModel.PageSize);
        }
    }
}
