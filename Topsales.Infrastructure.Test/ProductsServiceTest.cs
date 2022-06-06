using Microsoft.Extensions.Configuration;
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
        private readonly IConfigurationRoot config;
        private readonly HttpClient client;
        private readonly ProductsService productsService;

        public ProductsServiceTest()
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
            productsService = new ProductsService(client, config);
        }

        [Theory]
        [InlineData("001201", "120675")]
        public async Task GetsAllSpecifiedProducts(string merchantProductNo1, string merchantProductNo2)
        {                                 
            var productName = await productsService.GetProducts(new string[] { merchantProductNo1, merchantProductNo2 });

            Assert.NotNull(productName);
            Assert.NotEmpty(productName);
        }

        [Theory]
        [InlineData( "001201", 25)]
        public async Task Updates_Product_Stocks( string merchantProductNo1, int stock)
        {   
            await productsService.UpdateStock(merchantProductNo1, stock);

            var products = await productsService.GetProducts(new string[] { merchantProductNo1 });

            Assert.Equal(25, products.First().Stock);
        }

    }
}
