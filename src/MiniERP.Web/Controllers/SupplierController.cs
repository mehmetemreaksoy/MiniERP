using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniERP.Web.Data;
using MiniERP.Web.Models;
using MiniERP.Web.Services;

namespace MiniERP.Web.Controllers;

[Authorize(Roles = "Admin")]
public class SupplierController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuditLogService _auditLogService;

    public SupplierController(AppDbContext context, IAuditLogService auditLogService)
    {
        _context = context;
        _auditLogService = auditLogService;
    }

    public IActionResult Index()
    {
        var suppliers = _context.Suppliers
            .Where(s => !s.IsDeleted)
            .ToList();

        return View(suppliers);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Supplier supplier)
    {
        if (!ModelState.IsValid)
        {
            var suppliers = _context.Suppliers
                .Where(s => !s.IsDeleted)
                .ToList();

            return View(nameof(Index), suppliers);
        }

        _context.Suppliers.Add(supplier);
        _context.SaveChanges();

        _auditLogService.Log(
            "Create",
            "Supplier",
            supplier.Id,
            $"Tedarikçi oluşturuldu: {supplier.Name}");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Supplier supplier)
    {
        if (!ModelState.IsValid)
        {
            var suppliers = _context.Suppliers
                .Where(s => !s.IsDeleted)
                .ToList();

            return View(nameof(Index), suppliers);
        }

        _context.Suppliers.Update(supplier);
        _context.SaveChanges();

        _auditLogService.Log(
            "Edit",
            "Supplier",
            supplier.Id,
            $"Tedarikçi güncellendi: {supplier.Name}");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var supplier = _context.Suppliers.Find(id);

        if (supplier == null)
        {
            return NotFound();
        }

        supplier.IsDeleted = true;
        supplier.DeletedDate = DateTime.Now;
        _context.SaveChanges();

        _auditLogService.Log(
            "Delete",
            "Supplier",
            supplier.Id,
            $"Tedarikçi silindi: {supplier.Name}");

        return RedirectToAction(nameof(Index));
    }
}
