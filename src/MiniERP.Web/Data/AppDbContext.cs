using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniERP.Web.Models;

namespace MiniERP.Web.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }

    public DbSet<StockMovement> StockMovements { get; set; }

    public DbSet<Sale> Sales { get; set; }

    public DbSet<Purchase> Purchases { get; set; }

    public DbSet<SalesInvoice> SalesInvoices { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Sale>()
            .Property(s => s.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Sale>()
            .Property(s => s.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<SalesInvoice>()
            .Property(i => i.SubTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<SalesInvoice>()
            .Property(i => i.VatRate)
            .HasPrecision(5, 2);

        modelBuilder.Entity<SalesInvoice>()
            .Property(i => i.VatAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<SalesInvoice>()
            .Property(i => i.GrandTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<SalesInvoice>()
            .HasOne(i => i.Sale)
            .WithOne(s => s.SalesInvoice)
            .HasForeignKey<SalesInvoice>(i => i.SaleId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SalesInvoice>()
            .HasOne(i => i.Customer)
            .WithMany(c => c.SalesInvoices)
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
