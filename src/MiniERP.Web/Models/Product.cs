namespace MiniERP.Web.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int CriticalStockLevel { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public List<StockMovement> StockMovements { get; set; } = new();

    public List<Sale> Sales { get; set; } = new();
}
