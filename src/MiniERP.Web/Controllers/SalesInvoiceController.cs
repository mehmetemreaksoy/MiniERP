using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniERP.Web.Data;
using MiniERP.Web.Models;
using MiniERP.Web.Services;

namespace MiniERP.Web.Controllers;

[Authorize(Roles = "Admin,SalesUser")]
public class SalesInvoiceController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuditLogService _auditLogService;

    public SalesInvoiceController(AppDbContext context, IAuditLogService auditLogService)
    {
        _context = context;
        _auditLogService = auditLogService;
    }

    public IActionResult Index()
    {
        var invoices = _context.SalesInvoices
            .Include(i => i.Customer)
            .Include(i => i.Sale)
                .ThenInclude(s => s!.Product)
            .ToList();

        LoadSales();

        return View(invoices);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(int saleId)
    {
        var sale = _context.Sales
            .Include(s => s.Customer)
            .FirstOrDefault(s => s.Id == saleId && s.Status == "Active");

        if (sale == null)
        {
            ModelState.AddModelError("SaleId", "Aktif satış bulunamadı.");
        }

        var hasInvoice = _context.SalesInvoices.Any(i => i.SaleId == saleId);

        if (hasInvoice)
        {
            ModelState.AddModelError("SaleId", "Bu satış için daha önce fatura oluşturulmuş.");
        }

        if (!ModelState.IsValid)
        {
            var invoices = _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.Sale)
                    .ThenInclude(s => s!.Product)
                .ToList();

            LoadSales();

            return View(nameof(Index), invoices);
        }

        var subTotal = sale!.TotalPrice;
        var vatRate = 20m;
        var vatAmount = subTotal * 0.20m;
        var invoiceNumber = GenerateInvoiceNumber();

        var salesInvoice = new SalesInvoice
        {
            InvoiceNumber = invoiceNumber,
            SaleId = sale.Id,
            CustomerId = sale.CustomerId,
            InvoiceDate = DateTime.Now,
            SubTotal = subTotal,
            VatRate = vatRate,
            VatAmount = vatAmount,
            GrandTotal = subTotal + vatAmount,
            Status = "Issued"
        };

        _context.SalesInvoices.Add(salesInvoice);
        _context.SaveChanges();

        _auditLogService.Log(
            "Create",
            "SalesInvoice",
            salesInvoice.Id,
            $"Satış faturası oluşturuldu: {salesInvoice.InvoiceNumber}");

        return RedirectToAction(nameof(Index));
    }

    private void LoadSales()
    {
        var invoicedSaleIds = _context.SalesInvoices.Select(i => i.SaleId).ToList();

        var sales = _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.Product)
            .Where(s => s.Status == "Active" && !invoicedSaleIds.Contains(s.Id))
            .ToList()
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Customer?.Name} - {s.Product?.Name} / {s.TotalPrice:N2}"
            })
            .ToList();

        ViewBag.Sales = sales;
    }

    private string GenerateInvoiceNumber()
    {
        var year = DateTime.Now.Year;
        var nextNumber = _context.SalesInvoices.Count(i => i.InvoiceDate.Year == year) + 1;

        return $"INV-{year}-{nextNumber:0000}";
    }
}
