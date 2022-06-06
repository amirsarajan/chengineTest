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

        public async Task<IList<Sale>> GetTopSales(int top = 5)
        {
            var orders = await ordersService.GetOrders();
            var orderLines = orders.SelectMany(order => order.Lines);

            var topSales = orderLines.GroupBy(
                orderline => new
                {
                    orderline.Gtin,
                    orderline.MerchantProductNo
                }).Select(
                orderLineGroup => new
                {
                    orderLineGroup.Key.Gtin,
                    orderLineGroup.Key.MerchantProductNo,
                    SoldQuantity = orderLineGroup.Sum(orderLine => orderLine.Quantity)
                }).OrderByDescending(
                sale => sale.SoldQuantity
                ).Take(top).ToList();

            var topSoldMerchantProductNos = topSales.Select(topSold => topSold.MerchantProductNo);

            var topSoldProducts = await productsService.GetProducts(topSoldMerchantProductNos);

            return topSales.Join(topSoldProducts, 
                topsale => topsale.MerchantProductNo, 
                topProduct => topProduct.MerchantProductNo, 
                (topSale, topProduct) => new Sale{
                    MerchantProductNo = topProduct.MerchantProductNo,
                    GTIN = topSale.Gtin,
                    ProductName = topProduct.Name,
                    SoldQuantity = topSale.SoldQuantity
                }).ToList();
        }

    }
}