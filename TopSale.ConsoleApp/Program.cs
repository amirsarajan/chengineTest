// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TopSale.ConsoleApp;
using Topsales.Infrastructure;
using TopSales.Core;

var services = new ServiceCollection();

services.Configure<ChannelEngineConfig>(config => config.ApiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322");

IConfigurationRoot config = ConfigBuilder.Build(mockApi:true);

services.AddExternalServices(config, "https://api-dev.channelengine.net/api/");
services.AddServices();

var provider = services.BuildServiceProvider();

var saleService = provider.GetRequiredService<ISalesService>();
var productsService = provider.GetRequiredService<IProductsService>();

var topSales = (await saleService.GetTopSales()).ToList();

Console.WriteLine("Top sold products");

topSales.ForEach(sale =>
{
    Console.WriteLine($"{sale.SoldQuantity} {sale.GTIN} {sale.ProductName}");
});

if (topSales.Any())
{
    Console.WriteLine($"Updating the product {topSales.First().ProductName} stock to 25 ...");
    await productsService.UpdateStock(topSales.First().MerchantProductNo, 25);
    var products = await productsService.GetProducts(new string[] { topSales.First().MerchantProductNo });
    Console.WriteLine("Updated product =>");
    Console.WriteLine(JsonConvert.SerializeObject(products.First(), Formatting.Indented));
}

