namespace Entities;

using Enums;

public class InventoryMovement
{
    public int Id { get; private set; }

    public int ProductId { get; private set; }

    public int UserId { get; private set; }

    public InventoryMovementType Type { get; private set; }

    public InventoryMovementConcept Concept { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal StockBefore { get; private set; }

    public decimal StockAfter { get; private set; }

    public int? ReferenceInvoiceId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    // Navegaciones
    public Product Product { get; private set; } = null!;

    public User User { get; private set; } = null!;

    private InventoryMovement()
    {
    }

    public InventoryMovement(
        int productId,
        int userId,
        InventoryMovementType type,
        InventoryMovementConcept concept,
        decimal quantity,
        decimal stockBefore,
        decimal stockAfter,
        int? referenceInvoiceId)
    {
        if (quantity <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");

        if (stockBefore < 0)
            throw new ArgumentException("El stock anterior no puede ser negativo.");

        if (stockAfter < 0)
            throw new ArgumentException("El stock posterior no puede ser negativo.");

        ProductId = productId;
        UserId = userId;
        Type = type;
        Concept = concept;
        Quantity = quantity;
        StockBefore = stockBefore;
        StockAfter = stockAfter;
        ReferenceInvoiceId = referenceInvoiceId;
        CreatedAt = DateTime.UtcNow;
    }
}
