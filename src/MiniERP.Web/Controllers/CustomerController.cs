using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniERP.Web.Data;
using MiniERP.Web.Models;
using MiniERP.Web.Services;

namespace MiniERP.Web.Controllers;

[Authorize(Roles = "Admin,SalesUser")]
public class CustomerController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuditLogService _auditLogService;

    public CustomerController(AppDbContext context, IAuditLogService auditLogService)
    {
        _context = context;
        _auditLogService = auditLogService;
    }

    public IActionResult Index()
    {
        var customers = _context.Customers.ToList();
        return View(customers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        _context.Customers.Add(customer);
        _context.SaveChanges();

        _auditLogService.Log(
            "Create",
            "Customer",
            customer.Id,
            $"Cari oluşturuldu: {customer.Name}");

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var customer = _context.Customers.Find(id);

        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        _context.Customers.Update(customer);
        _context.SaveChanges();

        _auditLogService.Log(
            "Edit",
            "Customer",
            customer.Id,
            $"Cari güncellendi: {customer.Name}");

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var customer = _context.Customers.Find(id);

        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var customer = _context.Customers.Find(id);

        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        _auditLogService.Log(
            "Delete",
            "Customer",
            customer.Id,
            $"Cari silindi: {customer.Name}");

        return RedirectToAction(nameof(Index));
    }
}
