using TopSales.Domain;

namespace TopSales.Core
{
    public class TestData
    {
        public static OrderLine CreateTestOrderLine(
       int productIndex, int quantity, IList<Product> products, string[] gtins)
        {
            return new OrderLine()
            {
                MerchantProductNo = products[productIndex].MerchantProductNo,
                Gtin = gtins[productIndex],
                Quantity = quantity,
            };
        }

        public static Tuple<IList<Product>, string[]> CreateTestProducts(int noProducts)
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

        public static List<Order> CreateTestOrders(int noOrders, IList<Product> products, string[] gtins)
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
}
