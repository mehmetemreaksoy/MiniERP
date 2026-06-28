namespace MiniERP.Web.Models;

public class SalesInvoice
{
    public int Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public int SaleId { get; set; }

    public Sale? Sale { get; set; }

    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public DateTime InvoiceDate { get; set; } = DateTime.Now;

    public decimal SubTotal { get; set; }

    public decimal VatRate { get; set; }

    public decimal VatAmount { get; set; }

    public decimal GrandTotal { get; set; }

    public string Status { get; set; } = "Issued";

    public DateTime? CancelledDate { get; set; }

    public string? CancelReason { get; set; }
}
