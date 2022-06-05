using System.Collections.Generic;
using Xunit;

namespace TopSalesTest;

public class TopSalesTest
{
    [Fact]
    public void ReturnsEmptyWhenNoOrderFound()
    {
        var orders = new List<Order>();
        var salesService = new SalesService();
        List<Sale> sales= salesService.GetTopSales(orders);

        Assert.Empty(sales);
    }
}