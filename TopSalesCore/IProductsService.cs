using TopSales.Domain;

namespace TopSales.Core
{
    public interface IProductsService
    {
        Task<IList<Product>> GetProducts(IEnumerable<string> topSoldMerchantProductNos);
        Task UpdateStock(string merchantProductNo, int stock);
    }
}