using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MiniERP.Web.Data;
using MiniERP.Web.Models;
using MiniERP.Web.ViewModels;

namespace MiniERP.Web.Controllers;

[Authorize(Roles = "Admin,Manager,Viewer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var dashboard = new DashboardViewModel
        {
            TotalCategories = _context.Categories.Count(),
            TotalProducts = _context.Products.Count(),
            TotalCustomers = _context.Customers.Count(),
            TotalSales = _context.Sales.Count(),
            CriticalStockProductCount = _context.Products.Count(p => p.StockQuantity <= p.CriticalStockLevel),
            CriticalStockProducts = _context.Products
                .Where(p => p.StockQuantity <= p.CriticalStockLevel)
                .OrderBy(p => p.StockQuantity)
                .Take(5)
                .ToList(),
            RecentSales = _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .OrderByDescending(s => s.SaleDate)
                .Take(5)
                .ToList(),
            RecentStockMovements = _context.StockMovements
                .Include(s => s.Product)
                .OrderByDescending(s => s.MovementDate)
                .Take(5)
                .ToList()
        };

        return View(dashboard);
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
