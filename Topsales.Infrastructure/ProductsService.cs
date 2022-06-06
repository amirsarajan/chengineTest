using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using TopSales.Core;
using TopSales.Domain;

namespace Topsales.Infrastructure
{
    public class ProductsService : IProductsService
    {
        private HttpClient client;
        private ChannelEngineConfig config;

        public ProductsService(
            HttpClient client,
            IOptions<ChannelEngineConfig> options)
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
                throw Errors.FaildToPerformAction(ResourceActions.GetProducts, url, content);

            var result = JsonConvert.DeserializeObject<Response<Product[]>>(content);

            if (result is null)
                throw Errors.FailedToExtract(ResourceActions.GetProducts, url, content);

            if (!result.Success)
                throw Errors.FaildToPerformAction(ResourceActions.GetProducts, result.Message, url, content);

            return result.Content;
        }

        public async Task UpdateStock(string merchantProductNo, int stock)
        {
            var url = $"v2/products/{merchantProductNo}?apikey={config.ApiKey}";

            var patchProduct = new JsonPatchDocument<Product>();
            patchProduct.Replace(p => p.Stock, stock);

            var jsonPatch = JsonConvert.SerializeObject(patchProduct);

            using var requestContent = new StringContent(jsonPatch, Encoding.UTF8, "application/json");

            var response = await client.PatchAsync(url, requestContent);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw Errors.FaildToPerformAction(ResourceActions.PatchProductStock, url, content);

            var result = JsonConvert.DeserializeObject<Response<ProductPatchContent>>(content);

            if (result is null)
                throw Errors.FailedToExtract(ResourceActions.PatchProductStock, url, content);

            if (!result.Success)
                throw Errors.FaildToPerformAction(ResourceActions.PatchProductStock,result.Message, url, content);
        }
    }
}