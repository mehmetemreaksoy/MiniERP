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

    public IActionResult Cancel(int id)
    {
        var purchase = _context.Purchases
            .Include(p => p.Supplier)
            .Include(p => p.Product)
            .FirstOrDefault(p => p.Id == id);

        if (purchase == null)
        {
            return NotFound();
        }

        return View(purchase);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Cancel(int id, string? cancelReason)
    {
        var purchase = _context.Purchases
            .Include(p => p.Supplier)
            .Include(p => p.Product)
            .FirstOrDefault(p => p.Id == id);

        if (purchase == null)
        {
            return NotFound();
        }

        if (purchase.Status == "Cancelled")
        {
            return RedirectToAction(nameof(Index));
        }

        if (purchase.Product == null)
        {
            ModelState.AddModelError(string.Empty, "Ürün bulunamadı.");
            return View(purchase);
        }

        if (purchase.Product.StockQuantity < purchase.Quantity)
        {
            ModelState.AddModelError(string.Empty, "Ürün stoğu satın alma miktarından düşük olduğu için iptal edilemez.");
            return View(purchase);
        }

        purchase.Product.StockQuantity -= purchase.Quantity;
        purchase.Status = "Cancelled";
        purchase.CancelledDate = DateTime.Now;
        purchase.CancelReason = cancelReason;

        _context.SaveChanges();

        _auditLogService.Log(
            "Cancel",
            "Purchase",
            purchase.Id,
            "Satın alma iptal edildi. Stok geri düşüldü.");

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
