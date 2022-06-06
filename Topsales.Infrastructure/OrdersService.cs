using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;
using TopSales.Core;
using TopSales.Domain;

namespace Topsales.Infrastructure
{
    public class OrdersService : IOrdersService
    {
        private readonly HttpClient client;
        private readonly string apiKey;

        public OrdersService(
            HttpClient client,
            IConfiguration configuration
            )
        {
            this.client = client;
            apiKey = configuration.GetValue<string>("ApiKey");

        }

        public async Task<IList<Order>> GetOrders()
        {
            var url = $"v2/orders?statuses=IN_PROGRESS&apikey={apiKey}";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw Errors.FaildToPerformAction(ResourceActions.GetOrders, url, content);

            var result = JsonSerializer.Deserialize<Response<Order[]>>(content);
            if (result is null)
                throw Errors.FailedToExtract(ResourceActions.GetOrders, url, content);
            if (!result.Success)
                throw Errors.FaildToPerformAction(ResourceActions.GetOrders, result.Message, url, content);
            return result.Content;
        }
    }
}
