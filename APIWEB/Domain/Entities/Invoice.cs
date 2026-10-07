using Enums;

namespace Entities;


public class Invoice
{
    public int Id { get; private set; }

    public int? ClientId { get; private set; }

    public int UserId { get; private set; }

    public string? ClientName { get; private set; }

    public string? ClientTaxId { get; private set; }

    public string? InvoiceNumber { get; private set; }

    public PaymentType PaymentType { get; private set; }

    public decimal Subtotal { get; private set; }

    public decimal TaxAmount { get; private set; }

    public decimal DiscountAmount { get; private set; }

    public decimal TotalAmount { get; private set; }

    public DateTime CreatedAt { get; private set; }

    // Navegaciones

    public Client? Client { get; private set; }

    public User User { get; private set; } = null!;

    public ICollection<InvoiceDetail> Details { get; private set; }
        = new List<InvoiceDetail>();

    private Invoice()
    {
    }

    public Invoice(
        int userId,
        int? clientId,
        PaymentType paymentType)
    {
        UserId = userId;
        ClientId = clientId;
        PaymentType = paymentType;

        Subtotal = 0;
        TaxAmount = 0;
        DiscountAmount = 0;
        TotalAmount = 0;

        CreatedAt = DateTime.UtcNow;
    }
}