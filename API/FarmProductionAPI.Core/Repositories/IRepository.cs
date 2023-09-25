using FarmProductionAPI.Core.PagingHelper;

namespace FarmProductionAPI.Core.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetById(Guid id);

        IQueryable<TEntity> GetAll();

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Remove(TEntity entity);
        Task<PaginatedList<TEntity>> GetAll(PagingAndSortingModel pagingAndSortingModel);
    }
}
