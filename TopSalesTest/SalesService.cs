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
            var orderLines = orders.SelectMany(order => order.Lines);

            return orderLines
                .GroupBy(orderline => new { orderline.GTIN, orderline.MerchantProductNo })
                .Select(orderLineGroup => new Sale()
                {
                    GTIN = orderLineGroup.Key.GTIN,
                    SoldQuantity = orderLineGroup.Sum(orderLine => orderLine.Quantity),
                    ProductName = GetProduct(products, orderLineGroup.Key.MerchantProductNo)?.Name

                })
                .OrderByDescending(sale => sale.SoldQuantity)
                .ToList();
        }

        private Product GetProduct(
            IList<Product> products, string merchantProductNo)
        {
            return products.SingleOrDefault(
                product => product.MerchantProductNo == merchantProductNo
                );
        }
    }
}