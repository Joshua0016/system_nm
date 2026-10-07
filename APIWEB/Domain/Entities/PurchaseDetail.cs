namespace Entities;

public class PurchaseDetail
{
    public int Id { get; private set; }

    public int PurchaseId { get; private set; }

    public int ProductId { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal UnitCost { get; private set; }

    public bool TaxIncluded { get; private set; }

    public decimal TaxRate { get; private set; }

    // Relaciones
    public Purchase Purchase { get; private set; } = null!;

    public Product Product { get; private set; } = null!;

    private PurchaseDetail()
    {
    }

    public PurchaseDetail(
        int productId,
        decimal quantity,
        decimal unitCost,
        bool taxIncluded,
        decimal taxRate)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "La cantidad debe ser mayor que cero.");

        if (unitCost <= 0)
            throw new ArgumentException(
                "El costo unitario debe ser mayor que cero.");

        if (taxRate < 0)
            throw new ArgumentException(
                "La tasa de impuesto no puede ser negativa.");

        ProductId = productId;
        Quantity = quantity;
        UnitCost = unitCost;
        TaxIncluded = taxIncluded;
        TaxRate = taxRate;
    }
}