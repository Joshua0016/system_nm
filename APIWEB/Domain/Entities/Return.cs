namespace Entities;

using Enums;

public class Return
{
    public int Id { get; private set; }

    public int InvoiceId { get; private set; }

    public int UserId { get; private set; }

    public string Ncf { get; private set; } = string.Empty;

    public string AffectedNcf { get; private set; } = string.Empty;

    public ReturnReasonCode ReasonCode { get; private set; }

    public decimal Subtotal { get; private set; }

    public decimal TaxAmount { get; private set; }

    public decimal Total { get; private set; }

    public DateTime CreatedAt { get; private set; }

    // Navegaciones
    public Invoice Invoice { get; private set; } = null!;

    public User User { get; private set; } = null!;

    public ICollection<ReturnDetail> Details { get; private set; }
        = new List<ReturnDetail>();

    private Return()
    {
    }

    public Return(
        int invoiceId,
        int userId,
        string ncf,
        string affectedNcf,
        ReturnReasonCode reasonCode)
    {
        InvoiceId = invoiceId;
        UserId = userId;
        Ncf = ncf;
        AffectedNcf = affectedNcf;
        ReasonCode = reasonCode;
        Subtotal = 0;
        TaxAmount = 0;
        Total = 0;
        CreatedAt = DateTime.UtcNow;
    }
}
