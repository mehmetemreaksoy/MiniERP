using Microsoft.EntityFrameworkCore;
using MiniERP.Web.Models;

namespace MiniERP.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<StockMovement> StockMovements { get; set; }

    public DbSet<Sale> Sales { get; set; }
}
