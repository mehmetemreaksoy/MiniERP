namespace MiniERP.Web.Models;

public class StockMovement
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public string MovementType { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public DateTime MovementDate { get; set; } = DateTime.Now;
}
