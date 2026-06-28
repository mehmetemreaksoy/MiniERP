using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniERP.Web.Data;
using MiniERP.Web.Models;
using MiniERP.Web.Services;

namespace MiniERP.Web.Controllers;

[Authorize(Roles = "Admin,WarehouseUser")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuditLogService _auditLogService;

    public ProductController(AppDbContext context, IAuditLogService auditLogService)
    {
        _context = context;
        _auditLogService = auditLogService;
    }

    public IActionResult Index()
    {
        var products = _context.Products
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted)
            .ToList();
        LoadCategories();

        return View(products);
    }

    public IActionResult Create()
    {
        LoadCategories();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            LoadCategories();
            return View(product);
        }

        _context.Products.Add(product);
        _context.SaveChanges();

        _auditLogService.Log(
            "Create",
            "Product",
            product.Id,
            $"Ürün oluşturuldu: {product.Name}");

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var product = _context.Products.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        LoadCategories();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            LoadCategories();
            return View(product);
        }

        _context.Products.Update(product);
        _context.SaveChanges();

        _auditLogService.Log(
            "Edit",
            "Product",
            product.Id,
            $"Ürün güncellendi: {product.Name}");

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var product = _context.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _context.Products.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        product.IsDeleted = true;
        product.DeletedDate = DateTime.Now;
        _context.SaveChanges();

        _auditLogService.Log(
            "Delete",
            "Product",
            product.Id,
            $"Ürün pasife alındı: {product.Name}");

        return RedirectToAction(nameof(Index));
    }

    private void LoadCategories()
    {
        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
    }
}
