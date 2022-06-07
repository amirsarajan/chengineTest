// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopSale.ConsoleApp;
using TopSales.Core;

var services = new ServiceCollection();
var config = CreateConfiguration(services);

services.AddConsoleAppServices(config);

var provider = services.BuildServiceProvider();
var app = provider.GetRequiredService<TopSalesConsoleApp>();

await app.ShowTopSales();
await app.UpdateTopProductStock(stock: 25);

static IConfigurationRoot CreateConfiguration(ServiceCollection services)
{
    var config = ConfigBuilder.Build<Program>(Environment.GetCommandLineArgs());
    services.AddSingleton<IConfiguration>(config);
    return config;
}