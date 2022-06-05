using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TopSalesTest;

public class TopSalesTest
{
    private readonly List<Product> products;
    private readonly SalesService salesService;

    public TopSalesTest()
    {
        products = new List<Product>()
        {
            new Product()
            {
                MerchantProductNo = "#1111",
                Name = " Product1",
            },
            new Product()
            {
                MerchantProductNo = "#2222",
                Name = " Product2",
            }
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
    public void Returns_Only_top_sales_of_multi_order_of_one_product()
    {
        var orderLine = new OrderLine()
        {
            MerchantProductNo = products[0].MerchantProductNo,
            GTIN = "G#1111",
            Quantity = 10
        };
        var orderLine2 = new OrderLine()
        {
            MerchantProductNo = products[1].MerchantProductNo,
            GTIN = "G#1111",
            Quantity = 15
        };
        var orders = new List<Order>() {
            new Order(){
                Status = "IN_PROGRESS",
                Lines = new List<OrderLine>(){ orderLine }
            },
            new Order(){
                Status = "IN_PROGRESS",
                Lines = new List<OrderLine>(){ orderLine2 }
            }
        };

        var topsales = salesService.GetTopSales(orders, products);

        Assert.Collection(topsales,
            theOnlySale =>
            {
                Assert.Equal(orderLine.Quantity + orderLine2.Quantity, theOnlySale.SoldQuantity);
                Assert.Equal(products.First().Name, theOnlySale.ProductName);
            });
    }

    [Fact]
    public void Returns_topsales_of_multiple_products()
    {
        var orderLine = new OrderLine()
        {
            MerchantProductNo = products[0].MerchantProductNo,
            GTIN = "G#1111",
            Quantity = 10
        };
        var orderLine2 = new OrderLine()
        {
            MerchantProductNo = products[1].MerchantProductNo,
            GTIN = "G#2222",
            Quantity = 15
        };
        var orders = new List<Order>() {
            new Order(){
                Status = "IN_PROGRESS",
                Lines = new List<OrderLine>(){ 
                    orderLine ,
                    orderLine2
                }
            }
        };

        var topsales = salesService.GetTopSales(orders, products);

        Assert.Collection(topsales,
            firstTopSale =>
            {
                Assert.Equal(orderLine2.Quantity, firstTopSale.SoldQuantity);
                Assert.Equal(products[1].Name, firstTopSale.ProductName);
            },
            secondTopSale =>
            {
                Assert.Equal(orderLine.Quantity , secondTopSale.SoldQuantity);
                Assert.Equal(products[0].Name, secondTopSale.ProductName);
            });
    }
}