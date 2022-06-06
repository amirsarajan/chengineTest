using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Topsales.Infrastructure.Integration.Test
{
    public class ProductsServiceTest
    {
        private const string BaseAddress = "https://api-dev.channelengine.net/api/";

        [Theory]
        [InlineData("541b989ef78ccb1bad630ea5b85c6ebff9ca3322", "001201", "120675")]
        public async Task GetsAllSpecifiedProducts(string apiKey, string merchantProductNo1, string merchantProductNo2)
        {
            using var client = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };
            var options = Options.Create(new ChannelEngineConfig()
            {
                ApiKey = apiKey
            });
            var productsService = new ProductsService(client, options);
            var productName = await productsService.GetProducts(new string[] { merchantProductNo1, merchantProductNo2 });

            Assert.NotNull(productName);
            Assert.NotEmpty(productName);
        }

        [Theory]
        [InlineData("541b989ef78ccb1bad630ea5b85c6ebff9ca3322", "001201", 25)]
        public async Task Updates_Product_Stocks(string apiKey, string merchantProductNo1, int stock)
        {
            using var client = new HttpClient() { BaseAddress = new Uri(BaseAddress) };
           
            var options = Options.Create(new ChannelEngineConfig() { ApiKey = apiKey });
            
            var productsService = new ProductsService(client, options);

             await productsService.UpdateStock(merchantProductNo1, stock);

            var product = await productsService.GetProducts(new string[] { merchantProductNo1 });
        }
    }
}
