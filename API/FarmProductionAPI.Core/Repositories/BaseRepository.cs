using FarmProductionAPI.Core.PagingHelper;
using FarmProductionAPI.Domain;
using FarmProductionAPI.Domain.Models;
using Microsoft.AspNetCore.WebUtilities;

using Microsoft.EntityFrameworkCore;

namespace FarmProductionAPI.Core.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {

        protected readonly DataContext _dataContext;
        protected IQueryable<TEntity> AggregateRoots;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            AggregateRoots = dataContext.Set<TEntity>()
                .AsQueryable();
        }



        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await AggregateRoots.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return AggregateRoots.AsQueryable();
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var insertedItem = _dataContext.Set<TEntity>()
                .Add(entity)
                .Entity;

            await _dataContext.SaveChangesAsync();
            return insertedItem;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var updatedItem = _dataContext.Set<TEntity>()
                .Update(entity).Entity;

            await _dataContext.SaveChangesAsync();

            return updatedItem;
        }

        public virtual async Task Remove(TEntity entity)
        {
            _dataContext.Set<TEntity>()
                .Remove(entity);

            await _dataContext.SaveChangesAsync();
        }

        public virtual async Task<PaginatedList<TEntity>> GetAll(PagingAndSortingModel pagingAndSortingModel)
        {
            var itemsQuerys = QueryHelper.BuildOrderExpression(AggregateRoots, pagingAndSortingModel);
            // itemsQuery = QueryHelper.BuildFilterExpressions(itemsQuery, pagingAndSortingModel);

            var itemsQuery = AggregateRoots.OfType<TEntity>()
                .AsQueryable();

            return await PaginatedList<TEntity>.CreateAsync(itemsQuerys, pagingAndSortingModel.PageIndex, pagingAndSortingModel.PageSize);
        }
    }
}
