using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
namespace FarmProductionAPI.Core
{
    public class MediatrInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(IFarmProductionInfrastructureMarker), typeof(IFarmProductionInfrastructureMarker));
        }
    }
}
