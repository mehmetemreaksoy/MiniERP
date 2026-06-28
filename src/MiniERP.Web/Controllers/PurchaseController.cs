using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniERP.Web.Data;
using MiniERP.Web.Models;
using MiniERP.Web.Services;

namespace MiniERP.Web.Controllers;

[Authorize(Roles = "Admin")]
public class PurchaseController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuditLogService _auditLogService;

    public PurchaseController(AppDbContext context, IAuditLogService auditLogService)
    {
        _context = context;
        _auditLogService = auditLogService;
    }

    public IActionResult Index()
    {
        var purchases = _context.Purchases
            .Include(p => p.Supplier)
            .Include(p => p.Product)
            .ToList();

        LoadSuppliers();
        LoadProducts();

        return View(purchases);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Purchase purchase)
    {
        var supplier = _context.Suppliers.FirstOrDefault(s => s.Id == purchase.SupplierId && !s.IsDeleted);
        var product = _context.Products.FirstOrDefault(p => p.Id == purchase.ProductId && !p.IsDeleted);

        if (supplier == null)
        {
            ModelState.AddModelError("SupplierId", "Supplier not found.");
        }

        if (product == null)
        {
            ModelState.AddModelError("ProductId", "Product not found.");
        }

        if (!ModelState.IsValid)
        {
            var purchases = _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Product)
                .ToList();

            LoadSuppliers();
            LoadProducts();

            return View(nameof(Index), purchases);
        }

        purchase.TotalPrice = purchase.Quantity * purchase.UnitPrice;
        purchase.Status = "Active";
        product!.StockQuantity += purchase.Quantity;

        _context.Purchases.Add(purchase);
        _context.SaveChanges();

        _auditLogService.Log(
            "Create",
            "Purchase",
            purchase.Id,
            "Satın alma oluşturuldu. Stok artırıldı.");

        return RedirectToAction(nameof(Index));
    }

    private void LoadSuppliers()
    {
        ViewBag.Suppliers = new SelectList(_context.Suppliers.Where(s => !s.IsDeleted).ToList(), "Id", "Name");
    }

    private void LoadProducts()
    {
        ViewBag.Products = new SelectList(_context.Products.Where(p => !p.IsDeleted).ToList(), "Id", "Name");
    }
}
