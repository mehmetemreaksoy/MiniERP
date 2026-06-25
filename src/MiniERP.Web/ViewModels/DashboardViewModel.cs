using MiniERP.Web.Models;

namespace MiniERP.Web.ViewModels;

public class DashboardViewModel
{
    public int TotalCategories { get; set; }

    public int TotalProducts { get; set; }

    public int TotalCustomers { get; set; }

    public int TotalSales { get; set; }

    public int CriticalStockProductCount { get; set; }

    public List<Sale> RecentSales { get; set; } = new();

    public List<StockMovement> RecentStockMovements { get; set; } = new();

    public List<Product> CriticalStockProducts { get; set; } = new();
}
