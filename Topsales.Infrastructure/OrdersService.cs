using Microsoft.Extensions.Options;
using System.Text.Json;
using TopSales.Core;
using TopSales.Domain;

namespace Topsales.Infrastructure
{
    public class OrdersService : IOrdersService
    {
        private readonly HttpClient client;
        private readonly ChannelEngineConfig config;

        public OrdersService(
            HttpClient client,
            IOptions<ChannelEngineConfig> config
            )
        {
            this.client = client;
            this.config = config.Value;
        }

        public async Task<IList<Order>> GetOrders()
        {
            var url = $"v2/orders?statuses=IN_PROGRESS&apikey={config.ApiKey}";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw Erros.FaildToPerformAction(ResourceActions.GetOrders, url, content);

            var result = JsonSerializer.Deserialize<Response<Order[]>>(content);
            if (result is null)
                throw Erros.FailedToExtract(ResourceActions.GetOrders, url, content);
            if (!result.Success)
                throw Erros.FaildToPerformAction(ResourceActions.GetOrders, result.Message, url, content);
            return result.Content;
        }
    }
}
