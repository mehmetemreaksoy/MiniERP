namespace MiniERP.Web.Models;

public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public List<Sale> Sales { get; set; } = new();
}
