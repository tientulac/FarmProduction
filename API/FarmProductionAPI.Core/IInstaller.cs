using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FarmProductionAPI.Core
{
    public interface IInstaller
    {
        void InstallerServices(IServiceCollection services, IConfiguration configuration);
    }
}
