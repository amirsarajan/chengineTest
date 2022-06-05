using TopSales.Domain;

namespace TopSales.Core
{
    public interface ISalesService
    {
        Task<IList<Sale>> GetTopSales(int top = 5);
    }
}