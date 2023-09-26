using FarmProductionAPI.Domain;
using EFCore.BulkExtensions;

namespace FarmProductionAPI.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken token = default)
        {
            await _dataContext.SaveChangesAsync(token);

            return true;
        }

        public async Task BulkSaveChangesAsync(CancellationToken token = default)
        {
            await _dataContext.BulkSaveChangesAsync(cancellationToken: token);
        }
    }
}
