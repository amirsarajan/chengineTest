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
            Product product)
        {
            var orderLines = orders.SelectMany(order => order.Lines);

            return orderLines
                .GroupBy(orderline => orderline.GTIN)
                .Select(productOrderLines => new Sale()
                {
                    GTIN = productOrderLines.Key,
                    SoldQuantity = productOrderLines.Sum(orderLine => orderLine.Quantity),
                    ProductName = product.Name

                }).ToList();
            
        }
    }
}