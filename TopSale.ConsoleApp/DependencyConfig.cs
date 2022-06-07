using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Topsales.Infrastructure;
using TopSales.Core;

namespace TopSale.ConsoleApp
{
    public static class DependencyConfig
    {
        public static void AddConsoleAppServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddExternalServices(config);
            services.AddCoreServices();
            services.AddSingleton<TopSalesConsoleApp>();
        }
        
    }
}
