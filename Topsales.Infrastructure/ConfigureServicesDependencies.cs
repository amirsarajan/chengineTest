using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopSales.Core;
using TopSales.Common;

namespace Topsales.Infrastructure
{
    public static class ConfigureServicesDependencies
    {
        public static void AddExternalServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration.ShouldMockAPI())
            {
                services.AddTransient<IOrdersService, MockOrdersService>();
                services.AddTransient<IProductsService, MockProductService>();
            }
            else
            {
                var baseUrl = configuration.GetValue<string>("BaseUrl");
                services.AddHttpClient<IOrdersService, OrdersService>(config => config.BaseAddress = new Uri(baseUrl));
                services.AddHttpClient<IProductsService, ProductsService>(config => config.BaseAddress = new Uri(baseUrl));
            }


        }
    }
}
