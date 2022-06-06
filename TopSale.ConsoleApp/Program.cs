// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Topsales.Infrastructure;
using TopSales.Core;

var services = new ServiceCollection();

services.Configure<ChannelEngineConfig>(config => config.ApiKey= "541b989ef78ccb1bad630ea5b85c6ebff9ca3322");
services.AddExternalServices("https://api-dev.channelengine.net/api/");
services.AddServices();

var provider = services.BuildServiceProvider();
var saleService = provider.GetRequiredService<ISalesService>();

var topSales = (await saleService.GetTopSales()).ToList();

Console.WriteLine("Top sold products");

topSales.ForEach(sale => {
    Console.WriteLine($"{sale.SoldQuantity,30} {sale.GTIN,30} {sale.ProductName}");
});

Console.WriteLine("Hello, World!");

