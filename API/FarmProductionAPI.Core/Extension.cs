using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace FarmProductionAPI.Core
{
    public static class Extension
    {
        // By calling this method the program will find all the classes implemented the interface IInstaller
        public static void InstallServicesInAssembly<TAssembly>(this IServiceCollection services, IConfiguration configuration)
        {
            // Find all implement IInstaller interface and then DI them
            var installers = typeof(TAssembly).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();
            installers.ForEach(installer => installer.InstallerServices(services, configuration));
        }

        public static WebApplication ConfigureServicesInAssembly<TAssembly>(this WebApplication app)
        {
            // Find all implement IConfigurator interface and then DI them
            var configurators = typeof(TAssembly).Assembly.ExportedTypes.Where(x => typeof(IConfigurator).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IConfigurator>()
                .ToList();
            configurators.ForEach(configurator => configurator.Configure(app));

            return app;
        }
    }
}
