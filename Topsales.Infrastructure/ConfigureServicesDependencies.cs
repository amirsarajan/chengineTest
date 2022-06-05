using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSales.Core;

namespace Topsales.Infrastructure
{
    public static class ConfigureServicesDependencies
    {
        public static void AddExternalServices(this IServiceCollection services, string baseAddress)
        {
            services.AddHttpClient<IOrdersService, OrdersService>(config => config.BaseAddress = new Uri(baseAddress));
            services.AddHttpClient<IProductsService, ProductsService>(config => config.BaseAddress = new Uri(baseAddress));
        }
    }
}
