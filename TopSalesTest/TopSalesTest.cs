using System.Collections.Generic;
using Xunit;

namespace TopSalesTest;

public class TopSalesTest
{
    private readonly Product product1;
    private readonly List<Product> products;
    private readonly SalesService salesService;

    public TopSalesTest()
    {
        product1 = new Product()
        {
            MerchantProductNo = "#1111",
            Name = " Product1",
        };
        products = new List<Product>() {
            product1
        };
        salesService = new SalesService();
    }

    [Fact]
    public void Returns_Empty_When_No_Order_Found()
    {
        var orders = new List<Order>();
        var sales = salesService.GetTopSales(orders, products);

        Assert.Empty(sales);
    }

    [Fact]
    public void Returns_The_Only_Sold_Product_Order()
    {
        var orderLine = new OrderLine()
        {
            MerchantProductNo = "#1111",
            GTIN = "G#1111",
            Quantity = 10
        };
        var orders = new List<Order>() {
            new Order(){
                Status = "IN_PROGRESS",
                Lines = new List<OrderLine>(){ orderLine }
            }
        };

        var topsales = salesService.GetTopSales(orders, products);

        Assert.Collection(topsales,
            theOnlySale =>
            {
                Assert.Equal(orderLine.Quantity, theOnlySale.SoldQuantity);
                Assert.Equal(product1.Name, theOnlySale.ProductName);
            });
    }
}