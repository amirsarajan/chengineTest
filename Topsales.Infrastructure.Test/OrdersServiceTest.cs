using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Topsales.Infrastructure.Integration.Test
{
    public class OrdersServiceTest
    {
        private const string BaseAddress = "https://api-dev.channelengine.net/api/";
        private readonly IConfigurationRoot  config;
        private HttpClient client;

        public OrdersServiceTest()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ApiKey","541b989ef78ccb1bad630ea5b85c6ebff9ca3322")
            });
            config = configBuilder.Build();
            client = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        [Fact]   
        public async Task FetchesAllInprogressOrders()
        {                     
            var ordersService = new OrdersService(client,config);
            var orders = await ordersService.GetOrders();

            Assert.NotEmpty(orders);
        }
    }
}