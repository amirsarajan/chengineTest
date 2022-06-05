using TopSales.Domain;

namespace TopSales.Core
{
    public interface IProductsService
    {
        Task<string> GetProductName(string merchantProductNo);
        Task<IList<Product>> GetProducts(IEnumerable<string> topSoldMerchantProductNos);
    }
}