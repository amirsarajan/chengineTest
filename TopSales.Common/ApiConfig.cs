
using Microsoft.Extensions.Configuration;

namespace TopSales.Common
{
    public static class ApiConfig
    {
        public static bool ShouldMockAPI(this IConfiguration config)
        {
            return config.GetValue<bool>("MOCK_API");
        }
    }
}
