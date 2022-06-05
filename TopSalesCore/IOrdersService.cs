using TopSales.Domain;

namespace TopSales.Core
{
    public interface IOrdersService
    {
        Task<IList<Order>> GetOrders();
    }
}
