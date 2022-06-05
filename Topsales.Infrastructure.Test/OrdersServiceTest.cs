using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Topsales.Infrastructure.Integration.Test
{
    public class OrdersServiceTest
    {
        private const string BaseAddress = "https://api-dev.channelengine.net/api/";

        public OrdersServiceTest()
        {
        }

        [Theory]
        [InlineData("541b989ef78ccb1bad630ea5b85c6ebff9ca3322")]
        public async Task FetchesAllInprogressOrders(string apiKey)
        {
            using var client = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };
            var options = Options.Create(new ChannelEngineConfig()
            {
                ApiKey = apiKey
            });
            var ordersService = new OrdersService(client, options);
            var orders = await ordersService.GetOrders();

            Assert.NotEmpty(orders);
        }
    }
}