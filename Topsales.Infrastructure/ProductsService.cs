using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using TopSales.Core;
using TopSales.Domain;

namespace Topsales.Infrastructure
{
    public class ProductsService: IProductsService
    {
        private HttpClient client;
        private ChannelEngineConfig config;

        public ProductsService(HttpClient client, IOptions<ChannelEngineConfig> options)
        {
            this.client = client;
            this.config = options.Value;
        }

        public async Task<IList<Product>> GetProducts(IEnumerable<string> topSoldMerchantProductNos)
        {
            var merchantProductNos = string.Join("&", topSoldMerchantProductNos.Select(no => $"merchantProductNoList={no}"));
            var url = $"v2/products?{merchantProductNos}&apikey={config.ApiKey}";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw Erros.FaildToGetProducts(url, content);

            var result = JsonSerializer.Deserialize<Response<Product>>(content);
            if (result is null)
                throw Erros.FailedToExtractProducts(url, content);
            if (!result.Success)
                throw Erros.FaildToGetProducts(result.Message, url, content);
            return result.Content;
        }
    }
}