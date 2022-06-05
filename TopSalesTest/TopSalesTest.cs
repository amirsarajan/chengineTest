using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TopSalesTest;

public class TopSalesTest
{
    private readonly List<Product> products;
    public string[] GTINs { get; }
    private readonly SalesService salesService;

    public TopSalesTest()
    {
        products = new List<Product>()
        {
            new Product()
            {
                MerchantProductNo = "#1111",
                Name = "Product1",
            },
            new Product()
            {
                MerchantProductNo = "#2222",
                Name = "Product2",
            },
            new Product()
            {
                MerchantProductNo = "#3333",
                Name = " Product3",
            }
        };
        GTINs = new string[] { "G#1111", "G#2222","G#3333" };
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
    public void Returns_the_only_top_sales_of_one_product()
    {
        var orderLine = new OrderLine()
        {
            MerchantProductNo = products[0].MerchantProductNo,
            GTIN = GTINs[0],
            Quantity = 10
        };
        var orderLine2 = new OrderLine()
        {
            MerchantProductNo = products[0].MerchantProductNo,
            GTIN = GTINs[0],
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
        var product1OrderLine = new OrderLine()
        {
            MerchantProductNo = products[0].MerchantProductNo,
            GTIN = GTINs[0],
            Quantity = 10
        };
        var product2OrderLine = new OrderLine()
        {
            MerchantProductNo = products[1].MerchantProductNo,
            GTIN = GTINs[1],
            Quantity = 15
        };
        var product3OrderLine = new OrderLine()
        {
            MerchantProductNo = products[2].MerchantProductNo,
            GTIN = GTINs[2],
            Quantity = 5,
        };
        var orders = new List<Order>() {
            new Order(){
                Status = "IN_PROGRESS",
                Lines = new List<OrderLine>(){
                    product1OrderLine ,
                    product2OrderLine
                }
            },
            new Order(){
                Status = "IN_PROGRESS",
                Lines = new List<OrderLine>(){
                    product3OrderLine
                }
            }
        };

        var topsales = salesService.GetTopSales(orders, products);

        Assert.Collection(topsales,
            firstTopSale =>
            {
                Assert.Equal(product2OrderLine.Quantity, firstTopSale.SoldQuantity);
                Assert.Equal(products[1].Name, firstTopSale.ProductName);
            },
            secondTopSale =>
            {
                Assert.Equal(product1OrderLine.Quantity, secondTopSale.SoldQuantity);
                Assert.Equal(products[0].Name, secondTopSale.ProductName);
            },
            thirdTopSales =>
            {
                Assert.Equal(product3OrderLine.Quantity, thirdTopSales.SoldQuantity);
                Assert.Equal(products[2].Name, thirdTopSales.ProductName);
            });
    }
}