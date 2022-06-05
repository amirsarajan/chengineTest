using TopSales.Domain;

namespace TopSales.Core
{
    public interface ISalesService
    {
        IList<Sale> GetTopSales(int top = 5);
    }
}