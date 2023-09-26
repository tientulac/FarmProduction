using Microsoft.AspNetCore.Builder;
namespace FarmProductionAPI.Core
{
    public interface IConfigurator
    {
        void Configure(WebApplication builder);
    }
}
