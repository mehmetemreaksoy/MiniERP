namespace MiniERP.Web.Models;

public class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? ContactPerson { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? TaxNumber { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? DeletedDate { get; set; }

    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
