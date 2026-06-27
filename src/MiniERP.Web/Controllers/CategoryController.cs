using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniERP.Web.Data;
using MiniERP.Web.Models;
using MiniERP.Web.Services;

namespace MiniERP.Web.Controllers;

[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuditLogService _auditLogService;

    public CategoryController(AppDbContext context, IAuditLogService auditLogService)
    {
        _context = context;
        _auditLogService = auditLogService;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        _context.Categories.Add(category);
        _context.SaveChanges();

        _auditLogService.Log(
            "Create",
            "Category",
            category.Id,
            $"Kategori oluşturuldu: {category.Name}");

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        _context.Categories.Update(category);
        _context.SaveChanges();

        _auditLogService.Log(
            "Edit",
            "Category",
            category.Id,
            $"Kategori güncellendi: {category.Name}");

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        _auditLogService.Log(
            "Delete",
            "Category",
            category.Id,
            $"Kategori silindi: {category.Name}");

        return RedirectToAction(nameof(Index));
    }
}
