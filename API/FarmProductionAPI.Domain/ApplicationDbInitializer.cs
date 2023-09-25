using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmProductionAPI.Domain
{
    public class ApplicationDbInitializer
    {
        private readonly DataContext _dbContext;


        private readonly ILogger _logger;

        public ApplicationDbInitializer(DataContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext.Database.GetMigrations().Any())
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    _logger.Information("Applying Migrations...");
                }
            }

            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                _logger.Information("Connection to Database Succeeded.");
            }
        }
    }

}
