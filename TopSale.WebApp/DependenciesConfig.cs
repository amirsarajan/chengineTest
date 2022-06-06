using Topsales.Infrastructure;
using TopSales.Core;

namespace TopSale.WebApp
{
    public static class DependenciesConfig
    {
        public static void AddTopSalesServices(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddExternalServices(config);
            services.AddServices();
        }
    }
}
