using Microsoft.Extensions.Options;
using System.Net.Http;
using TopSales.Core;

namespace Topsales.Infrastructure
{
    public class ProductsService: IProductsService
    {
        private HttpClient client;
        private IOptions<ChannelEngineConfig> options;

        public ProductsService(HttpClient client, IOptions<ChannelEngineConfig> options)
        {
            this.client = client;
            this.options = options;
        }

        public async Task<string> GetProductName(string merchantProductNo)
        {
            throw new NotImplementedException();
        }
    }
}