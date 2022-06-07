using Microsoft.Extensions.DependencyInjection;


namespace TopSales.Core
{
    public static class ConfigureServicesDependencies
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<ISalesService, SalesService>();
        }
    }
}
