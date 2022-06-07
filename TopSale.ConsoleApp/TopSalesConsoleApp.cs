using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSales.Core;
using TopSales.Domain;

namespace TopSale.ConsoleApp
{
    public class TopSalesConsoleApp
    {
        private readonly ISalesService salesService;
        private readonly IProductsService productsService;

        public List<Sale> topSales { get; private set; } = new List<Sale>();

        public TopSalesConsoleApp(
            ISalesService salesService,
            IProductsService productsService)
        {
            this.salesService = salesService;
            this.productsService = productsService;
        }

        public async Task ShowTopSales()
        {
            topSales.Clear();
            topSales.AddRange(await salesService.GetTopSales());

            DisplaySales(topSales);
        }
        public async Task UpdateTopProductStock(int stock =25)
        {
            Console.WriteLine();
            Console.WriteLine($"Updating the product {topSales.First().ProductName} stock to 25 ...");
          
            await productsService.UpdateStock(topSales.First().MerchantProductNo, stock);

            Console.WriteLine("Retrieving the Product ...");

            var products = await productsService.GetProducts(new string[] { topSales.First().MerchantProductNo });

            Console.WriteLine("Successfully updated.");

            DisplayProduct(products.First());
        }

        static void DisplaySales(List<Sale> topSales)
        {
            Console.WriteLine("Top sold products");

            topSales.ForEach(sale =>
            {
                Console.WriteLine($"{sale.SoldQuantity} {sale.GTIN} {sale.ProductName}");
            });
        }
        static void DisplayProduct(Product product)
        {
            Console.WriteLine();
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"MerchantProductNo: {product.MerchantProductNo}");
            Console.WriteLine($"Stock: {product.Stock}");
        }
    }
}
