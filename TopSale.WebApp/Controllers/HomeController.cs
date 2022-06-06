using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TopSale.WebApp.Models;
using TopSales.Core;

namespace TopSale.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ISalesService salesService;
    private readonly IProductsService productsService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ISalesService salesService,
        IProductsService productsService,
        ILogger<HomeController> logger)
    {
        this.salesService = salesService;
        this.productsService = productsService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var topOrders = await salesService.GetTopSales();
        return View(topOrders);
    }

    public async Task<IActionResult> Update(string merchantProductNo)
    {
        await productsService.UpdateStock(merchantProductNo, 25);
        var products = await productsService.GetProducts(new string[] { merchantProductNo });
        return View(products.First());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
