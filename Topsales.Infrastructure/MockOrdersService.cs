using TopSales.Core;
using TopSales.Domain;

namespace Topsales.Infrastructure
{
    public class MockOrdersService : IOrdersService
    {
        private readonly IList<Product> products;
        private readonly List<Order> orders;

        public MockOrdersService()
        {
            ( products, var gtins) = TestData.CreateTestProducts(10);
            orders = TestData.CreateTestOrders(20, products, gtins);
        }
        public Task<IList<Order>> GetOrders()
        {
            return Task.FromResult<IList<Order>>(orders);
        }
    }
}
