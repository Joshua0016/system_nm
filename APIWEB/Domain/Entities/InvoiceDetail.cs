namespace Entities;

public class InvoiceDetail
{
    public int Id { get; private set; }

    public int InvoiceId { get; private set; }

    public int ProductId { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TaxRate { get; private set; }

    public decimal Subtotal { get; private set; }

    // Navegaciones

    public Invoice Invoice { get; private set; } = null!;

    public Product Product { get; private set; } = null!;

    private InvoiceDetail()
    {
    }

    public InvoiceDetail(
        int productId,
        decimal quantity,
        decimal unitPrice,
        decimal taxRate,
        decimal subtotal)
    {
        if (quantity <= 0)
            throw new ArgumentException(
                "La cantidad debe ser mayor que cero.");

        if (unitPrice <= 0)
            throw new ArgumentException(
                "El precio unitario debe ser mayor que cero.");

        if (taxRate < 0)
            throw new ArgumentException(
                "La tasa de impuesto no puede ser negativa.");

        if (subtotal < 0)
            throw new ArgumentException(
                "El subtotal no puede ser negativo.");

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TaxRate = taxRate;
        Subtotal = subtotal;
    }
}