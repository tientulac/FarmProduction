using FarmProductionAPI.Core.PagingHelper;
using FarmProductionAPI.Domain;
using FarmProductionAPI.Domain.Models;
using Microsoft.AspNetCore.WebUtilities;

using Microsoft.EntityFrameworkCore;
using OneOf;
using System.Data.SqlTypes;
using System.Linq.Expressions;

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

        public virtual IQueryable<TEntity> GetAllUserSite()
        {
            return _queryAble.AsQueryable().Where(x => x.IsSoftDeleted != true);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            var insertedItem = _dbContext.Set<TEntity>()
                .Add(entity)
                .Entity;
            await _dbContext.SaveChangesAsync();
            return insertedItem;
        }

        public virtual async Task<TEntity> Update(TEntity entity, TEntity source)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.CreatedAt = source.CreatedAt;
            entity.DeletedAt = source.DeletedAt;
            var updatedItem = _dbContext.Set<TEntity>().Update(entity).Entity;
            await _dbContext.SaveChangesAsync();
            return updatedItem;
        }

        public virtual async Task Remove(TEntity entity)
        {
            entity.DeletedAt = DateTime.Now;
            entity.IsSoftDeleted = true;
            _dbContext.Set<TEntity>()
                .Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<PaginatedList<TEntity>> GetPaginated(PagingAndSortingModel pagingAndSortingModel)
        {
            var itemsQuerys = QueryHelper.BuildOrderExpression(_queryAble, pagingAndSortingModel);
            return await PaginatedList<TEntity>.CreateAsync(itemsQuerys, pagingAndSortingModel.PageIndex, pagingAndSortingModel.PageSize);
        }

        public Task<TEntity> GetByIdAsync(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? specialAction = null, CancellationToken token = default)
        {
            return GetFirstByConditionAsync(it => it.Id == id, specialAction, token);
        }

        public Task<TEntity> GetFirstByConditionAsync(
            Expression<Func<TEntity, bool>>? conditionFunction = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? specialAction = null,
            CancellationToken token = default)
        {
            var dataWithSpecialAction = specialAction?.Invoke(_queryAble) ?? _queryAble;

            return conditionFunction is null ? dataWithSpecialAction.FirstOrDefaultAsync(token) : dataWithSpecialAction.FirstOrDefaultAsync(conditionFunction, token);
        }

        public Task<bool> ExistByConditionAsync(Expression<Func<TEntity, bool>>? conditionFunction = null, CancellationToken token = default)
        {
            return conditionFunction is null ? _queryAble.AsQueryable().AnyAsync(token) : _queryAble.AnyAsync(conditionFunction, token);
        }

        public Task<List<TEntity>> GetManyByConditionAsync(
            Expression<Func<TEntity, bool>>? conditionFunction = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? specialAction = null,
            CancellationToken token = default)
        {
            var dataWithCondition = conditionFunction is null ? _queryAble : _queryAble.Where(conditionFunction);
            var dataWithSpecialAction = specialAction?.Invoke(dataWithCondition) ?? dataWithCondition;

            return dataWithSpecialAction.ToListAsync(token);
        }

        public Task<int> CountByConditionAsync(Expression<Func<TEntity, bool>>? conditionFunction = null, CancellationToken token = default)
        {
            return (conditionFunction is null ? _queryAble : _queryAble.Where(conditionFunction)).CountAsync(token);
        }

        public async Task<OneOf<TEntity, Exception>> CreateOneAsync(TEntity item, CancellationToken token = default)
        {
            if (item is null) return OneOf<TEntity, Exception>.FromT1(new SqlNullValueException("Cannot add null item!"));
            try
            {
                item.CreatedAt = DateTime.Now;
                var result = await _dbContext.AddAsync(item, token);

                return result.Entity;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public async Task<OneOf<bool, Exception>> CreateManyAsync(List<TEntity> items, CancellationToken token = default)
        {
            if (items is not { Count: > 0 }) return false;
            try
            {
                items.ForEach(x =>
                {
                    x.CreatedAt = DateTime.Now;
                });
                await _dbContext.AddRangeAsync(items, token);
                return true;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public async Task<OneOf<bool, Exception>> RemoveOneAsync(OneOf<TEntity, Expression<Func<TEntity, bool>>> itemOrSearchExpression)
        {
            var item = await itemOrSearchExpression.Match(Task.FromResult, f => GetFirstByConditionAsync(f));

            if (item is null) return false;
            try
            {
                item.DeletedAt = DateTime.Now;
                item.IsSoftDeleted = true;
                _dbContext.Remove(item);
                return true;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public virtual async Task RemoveManyAsync(List<TEntity> entities, CancellationToken token = default)
        {
            _dbContext.Set<TEntity>()
                .RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
