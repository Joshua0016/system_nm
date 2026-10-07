namespace Entities;


public class Purchase
{
    public int Id { get; private set; }

    public string InvoiceNumber { get; private set; } = string.Empty;

    public int SupplierId { get; private set; }

    public int UserId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    // Relaciones
    public Supplier Supplier { get; private set; } = null!;

    public User User { get; private set; } = null!;

    public ICollection<PurchaseDetail> Details { get; private set; }
        = new List<PurchaseDetail>();

    private Purchase()
    {
    }

    public Purchase(
        string invoiceNumber,
        int supplierId,
        int userId)
    {
        InvoiceNumber = invoiceNumber;
        SupplierId = supplierId;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
}