using TopSales.Domain;

namespace TopSales.Core
{
    public class SalesService : ISalesService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;

        public SalesService(
            IOrdersService ordersService,
            IProductsService productsService
            )
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }

        public IList<Sale> GetTopSales(int top = 5)
        {
            var orders = ordersService.GetOrders();
            var orderLines = orders.SelectMany(order => order.Lines);

            return orderLines
                .GroupBy(orderline => new { orderline.GTIN, orderline.MerchantProductNo })
                .Select(orderLineGroup => new Sale()
                {
                    GTIN = orderLineGroup.Key.GTIN,
                    SoldQuantity = orderLineGroup.Sum(orderLine => orderLine.Quantity),
                    ProductName = productsService.GetProductName(orderLineGroup.Key.MerchantProductNo)
                })
                .OrderByDescending(sale => sale.SoldQuantity)
                .Take(top)
                .ToList();
        }
       
    }
}