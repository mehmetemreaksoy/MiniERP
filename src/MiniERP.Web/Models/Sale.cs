namespace MiniERP.Web.Models;

public class Sale
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime SaleDate { get; set; } = DateTime.Now;

    public string Status { get; set; } = "Active";

    public DateTime? CancelledDate { get; set; }

    public string? CancelReason { get; set; }

    public SalesInvoice? SalesInvoice { get; set; }
}
