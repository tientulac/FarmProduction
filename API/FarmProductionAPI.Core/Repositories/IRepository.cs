using FarmProductionAPI.Core.PagingHelper;
using OneOf;
using System.Linq.Expressions;

namespace FarmProductionAPI.Core.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetById(Guid id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllUserSite();
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity, TEntity source);
        Task Remove(TEntity entity);
        Task<PaginatedList<TEntity>> GetPaginated(PagingAndSortingModel pagingAndSortingModel);
        Task<TEntity> GetByIdAsync(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? specialAction = null, CancellationToken token = default);
        Task<TEntity> GetFirstByConditionAsync(Expression<Func<TEntity, bool>>? conditionFunction = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? specialAction = null, CancellationToken token = default);
        Task<bool> ExistByConditionAsync(Expression<Func<TEntity, bool>>? conditionFunction = null, CancellationToken token = default);
        Task<List<TEntity>> GetManyByConditionAsync(Expression<Func<TEntity, bool>>? conditionFunction = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? specialAction = null, CancellationToken token = default);
        Task<int> CountByConditionAsync(Expression<Func<TEntity, bool>>? conditionFunction = null, CancellationToken token = default);
        Task<OneOf<TEntity, Exception>> CreateOneAsync(TEntity item, CancellationToken token = default);
        Task<OneOf<bool, Exception>> CreateManyAsync(List<TEntity> items, CancellationToken token = default);
        Task<OneOf<bool, Exception>> RemoveOneAsync(OneOf<TEntity, Expression<Func<TEntity, bool>>> itemOrSearchExpression);
        Task RemoveManyAsync(List<TEntity> entities, CancellationToken token = default);
    }
}
