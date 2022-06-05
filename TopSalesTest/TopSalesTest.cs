using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        GTINs = new string[] { "G#1111", "G#2222", "G#3333" };
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
        var orderLine = CreateTestOrderLine(0, 10, products, GTINs);
        var orderLine2 = CreateTestOrderLine(0, 15, products, GTINs);

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
        var testTuple = CreateTestProducts(10);

        var testProducts = testTuple.Item1;
        var testGtins = testTuple.Item2;

        var orders = CreateTestOrders(10, testProducts, testGtins);

        var topsales = salesService.GetTopSales(orders, testProducts).Take(5);

        var expectedSale = Enumerable.Range(1, 10)
            .Reverse()
            .Select(index => new Sale()
            {
                ProductName = testProducts[index - 1].Name,
                GTIN = testGtins[index - 1],
                SoldQuantity = index * 10
            }).ToList();

        Assert.Equal(expectedSale, topsales, new TopSaleComparer());
    }

    class TopSaleComparer : IEqualityComparer<Sale>
    {
        public bool Equals(Sale? x, Sale? y)
        {
            return x.GTIN == y.GTIN && x.ProductName == y.ProductName && x.SoldQuantity == y.SoldQuantity;
        }

        public int GetHashCode([DisallowNull] Sale obj)
        {
            return obj.GetHashCode();
        }
    }

    private static OrderLine CreateTestOrderLine(
        int productIndex, int quantity, IList<Product> products, string[] gtins)
    {
        return new OrderLine()
        {
            MerchantProductNo = products[productIndex].MerchantProductNo,
            GTIN = gtins[productIndex],
            Quantity = quantity,
        };
    }

    private Tuple<IList<Product>, string[]> CreateTestProducts(int noProducts)
    {
        return new Tuple<IList<Product>, string[]>(
            Enumerable.Range(1, noProducts)
            .Select(index =>
            {
                return new Product()
                {
                    MerchantProductNo = $"#{index}",
                    Name = $"Product{index}"
                };
            }).ToList(),
            Enumerable.Range(1, noProducts)
            .Select(index => $"G#{index}").ToArray());
    }

    private List<Order> CreateTestOrders(int noOrders, IList<Product> products, string[] gtins)
    {
        List<Order> orders = new List<Order>();
        for (var orderIndex = 0; orderIndex < noOrders; orderIndex++)
        {
            var orderLines = Enumerable.Range(0, products.Count())
                .Select(productIndex =>                
                    CreateTestOrderLine(productIndex, productIndex + 1, products, gtins)
                ).ToList();

            orders.Add(new Order()
            {
                Lines = orderLines
            });
        }
        return orders;
    }
}