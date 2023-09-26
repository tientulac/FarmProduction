namespace FarmProductionAPI.Core
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync(CancellationToken token = default);

        Task BulkSaveChangesAsync(CancellationToken token = default);
    }
}
