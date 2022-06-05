using System;
using System.Collections.Generic;
using System.Linq;

namespace TopSalesTest
{
    public class SalesService
    {
        public SalesService()
        {
        }

        public IList<Sale> GetTopSales(
            IList<Order> orders,
            IList<Product> products)
        {
            var orderLine = orders.SelectMany(order => order.Lines).First();

            var onlyProduct = products.First();

            return new List<Sale>(){
                new Sale()
                {
                    ProductName = onlyProduct.Name,
                    SoldQuantity = orderLine.Quantity,
                    GTIN = orderLine.GTIN,
                }
            };
        }
    }
}