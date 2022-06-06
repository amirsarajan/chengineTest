using System.Linq;
using TopSales.Core;
using TopSales.Domain;

namespace Topsales.Infrastructure
{
    public class MockProductService : IProductsService
    {
        private readonly IList<Product> products;

        public MockProductService()
        {
            (products, var gtins) = TestData.CreateTestProducts(10);
        }
        public Task<IList<Product>> GetProducts(IEnumerable<string> merchantProductNos)
        {
            return Task.FromResult<IList<Product>>(products.Where(product => merchantProductNos.Contains(product.MerchantProductNo)).ToList());
        }

        public Task UpdateStock(string merchantProductNo, int stock)
        {
            var product = products.SingleOrDefault(product => product.MerchantProductNo == merchantProductNo);
           
            product.Stock = stock;

            return Task.CompletedTask;
        }
    }
}