using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniERP.Web.Data;
using MiniERP.Web.Models;

namespace MiniERP.Web.Controllers;

[Authorize]
public class SaleController : Controller
{
    private readonly AppDbContext _context;

    public SaleController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var sales = _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.Product)
            .ToList();

        return View(sales);
    }

    public IActionResult Create()
    {
        LoadCustomers();
        LoadProducts();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Sale sale)
    {
        var product = _context.Products.Find(sale.ProductId);

        if (product == null)
        {
            ModelState.AddModelError("ProductId", "Product not found.");
        }
        else if (sale.Quantity > product.StockQuantity)
        {
            ModelState.AddModelError("Quantity", "Sale quantity cannot be greater than current stock.");
        }
        else
        {
            sale.UnitPrice = product.Price;
            sale.TotalPrice = sale.Quantity * sale.UnitPrice;
            product.StockQuantity -= sale.Quantity;
        }

        if (!ModelState.IsValid)
        {
            LoadCustomers();
            LoadProducts();
            return View(sale);
        }

        _context.Sales.Add(sale);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var sale = _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.Product)
            .FirstOrDefault(s => s.Id == id);

        if (sale == null)
        {
            return NotFound();
        }

        return View(sale);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var sale = _context.Sales.Find(id);

        if (sale == null)
        {
            return NotFound();
        }

        _context.Sales.Remove(sale);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    private void LoadCustomers()
    {
        ViewBag.Customers = new SelectList(_context.Customers.ToList(), "Id", "Name");
    }

    private void LoadProducts()
    {
        ViewBag.Products = new SelectList(_context.Products.ToList(), "Id", "Name");
    }
}
