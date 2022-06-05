using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using TopSales.Core;
using TopSales.Domain;
using Xunit;

namespace TopSalesTest;

public class TopSalesTest
{
    private readonly List<Product> products;
    public string[] GTINs { get; }
    private readonly SalesService salesService;
    private readonly Mock<IOrdersService> mockedOrderService = new Mock<IOrdersService>();
    private readonly Mock<IProductsService> mockedProductsService = new Mock<IProductsService>();

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

        mockedProductsService.Setup(productService => productService.GetProductName(It.IsAny<string>()))
            .Returns<string>(
            merchantProductNo => Task.FromResult(products.SingleOrDefault(p => p.MerchantProductNo == merchantProductNo)?.Name)
            );
        SetupMockeProductsService(products);

        salesService = new SalesService(mockedOrderService.Object, mockedProductsService.Object);
    }

    private void SetupMockeProductsService(IList<Product> products)
    {
        mockedProductsService.Setup(
            productService => productService.GetProducts(It.IsAny<IEnumerable<string>>()))
            .Returns<IEnumerable<string>>(merchantProductNos => Task.FromResult<IList<Product>>(products.Where(product => merchantProductNos.Contains(product.MerchantProductNo)).ToList()));
    }

    [Fact]
    public async Task Returns_Empty_When_No_Order_Found()
    {
        var orders = new List<Order>();
        mockedOrderService.Setup(orderService => orderService.GetOrders())
            .Returns(Task.FromResult<IList<Order>>(orders.ToList()));

        var sales = await salesService.GetTopSales();

        Assert.Empty(sales);
    }

    [Fact]
    public async Task Returns_the_only_top_sales_of_one_product()
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
        mockedOrderService.Setup(orderService => orderService.GetOrders())
            .Returns(Task.FromResult<IList<Order>>(orders.ToList()));

        var topsales = await salesService.GetTopSales();

        Assert.Collection(topsales,
            theOnlySale =>
            {
                Assert.Equal(orderLine.Quantity + orderLine2.Quantity, theOnlySale.SoldQuantity);
                Assert.Equal(products.First().Name, theOnlySale.ProductName);
            });
    }

    [Fact]
    public async Task Returns_topsales_of_multiple_products()
    {
        var testTuple = CreateTestProducts(10);

        var testProducts = testTuple.Item1;
        var testGtins = testTuple.Item2;

        var orders = CreateTestOrders(10, testProducts, testGtins);
        mockedOrderService.Setup(orderService => orderService.GetOrders())
            .Returns(Task.FromResult<IList<Order>>(orders.ToList()));

        SetupMockeProductsService(testProducts);

        var topsales = await salesService.GetTopSales();

        var expectedSale = Enumerable.Range(1, 10)
            .Reverse()
            .Select(index => new Sale()
            {
                ProductName = testProducts[index - 1].Name,
                GTIN = testGtins[index - 1],
                SoldQuantity = index * 10
            })
            .Take(5)
            .ToList();

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