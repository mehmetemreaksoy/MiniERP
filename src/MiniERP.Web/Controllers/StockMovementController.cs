using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniERP.Web.Data;
using MiniERP.Web.Models;

namespace MiniERP.Web.Controllers;

public class StockMovementController : Controller
{
    private readonly AppDbContext _context;

    public StockMovementController(AppDbContext context)
    {
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
        {
            context.Result = RedirectToAction("Login", "Account");
        }

        base.OnActionExecuting(context);
    }

    public IActionResult Index()
    {
        var stockMovements = _context.StockMovements
            .Include(s => s.Product)
            .ToList();

        return View(stockMovements);
    }

    public IActionResult Create()
    {
        LoadProducts();
        LoadMovementTypes();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(StockMovement stockMovement)
    {
        var product = _context.Products.Find(stockMovement.ProductId);

        if (product == null)
        {
            ModelState.AddModelError("ProductId", "Product not found.");
        }
        else if (stockMovement.MovementType == "In")
        {
            product.StockQuantity += stockMovement.Quantity;
        }
        else if (stockMovement.MovementType == "Out")
        {
            if (stockMovement.Quantity > product.StockQuantity)
            {
                ModelState.AddModelError("Quantity", "Output quantity cannot be greater than current stock.");
            }
            else
            {
                product.StockQuantity -= stockMovement.Quantity;
            }
        }
        else
        {
            ModelState.AddModelError("MovementType", "Please select a valid movement type.");
        }

        if (!ModelState.IsValid)
        {
            LoadProducts();
            LoadMovementTypes();
            return View(stockMovement);
        }

        _context.StockMovements.Add(stockMovement);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var stockMovement = _context.StockMovements
            .Include(s => s.Product)
            .FirstOrDefault(s => s.Id == id);

        if (stockMovement == null)
        {
            return NotFound();
        }

        return View(stockMovement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var stockMovement = _context.StockMovements.Find(id);

        if (stockMovement == null)
        {
            return NotFound();
        }

        _context.StockMovements.Remove(stockMovement);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    private void LoadProducts()
    {
        ViewBag.Products = new SelectList(_context.Products.ToList(), "Id", "Name");
    }

    private void LoadMovementTypes()
    {
        ViewBag.MovementTypes = new SelectList(new List<string> { "In", "Out" });
    }
}
