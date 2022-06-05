using Microsoft.Extensions.DependencyInjection;


namespace TopSales.Core
{
    public static class ConfigureServicesDependencies
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISalesService, SalesService>();
        }
    }
}
