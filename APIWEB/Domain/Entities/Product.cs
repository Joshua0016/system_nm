namespace SistemaFacturacion.Domain.Entities;

using SistemaFacturacion.Domain.Enums;

public class Product
{
    public const int CodeMaxLength = 50;
    public const int ReferenceMaxLength = 100;
    public const int DescriptionMaxLength = 200;

    public int Id { get; private set; }

    public string Code { get; private set; } = string.Empty;

    public string Reference { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Stock { get; private set; }

    // Fracción sobre el costo: 0.30 = 30 %
    public decimal Markup { get; private set; }

    public decimal Cost { get; private set; }

    // Siempre derivado: Cost × (1 + Markup)
    public decimal Price { get; private set; }

    public UnitMeasurement UnitMeasurement { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Product()
    {
    }

    public Product(
        string code,
        string reference,
        string description,
        decimal markup,
        UnitMeasurement unitMeasurement)
    {
        Code = ValidateText(code, CodeMaxLength, "El código");
        Reference = ValidateText(reference, ReferenceMaxLength, "La referencia");
        Description = ValidateText(description, DescriptionMaxLength, "La descripción");
        Markup = ValidateMarkup(markup);
        UnitMeasurement = ValidateUnitMeasurement(unitMeasurement);
        Stock = 0;
        Cost = 0;
        Price = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string code,
        string reference,
        string description,
        decimal markup,
        UnitMeasurement unitMeasurement)
    {
        Code = ValidateText(code, CodeMaxLength, "El código");
        Reference = ValidateText(reference, ReferenceMaxLength, "La referencia");
        Description = ValidateText(description, DescriptionMaxLength, "La descripción");
        Markup = ValidateMarkup(markup);
        UnitMeasurement = ValidateUnitMeasurement(unitMeasurement);
        RecalculatePrice();
    }

    // Lo usarán las compras al dar entrada a mercancía
    public void UpdateCost(decimal cost)
    {
        if (cost < 0)
            throw new ArgumentException("El costo no puede ser negativo.", nameof(cost));

        Cost = cost;
        RecalculatePrice();
    }

    private void RecalculatePrice()
    {
        Price = Math.Round(Cost * (1 + Markup), 2, MidpointRounding.AwayFromZero);
    }

    private static string ValidateText(string value, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{fieldName} es obligatorio.");

        value = value.Trim();

        if (value.Length > maxLength)
            throw new ArgumentException($"{fieldName} no puede superar {maxLength} caracteres.");

        return value;
    }

    private static decimal ValidateMarkup(decimal markup)
    {
        if (markup <= 0)
            throw new ArgumentException("El markup debe ser mayor que cero.", nameof(markup));

        return markup;
    }

    private static UnitMeasurement ValidateUnitMeasurement(UnitMeasurement unitMeasurement)
    {
        if (!Enum.IsDefined(unitMeasurement))
            throw new ArgumentException("Unidad de medida no válida.", nameof(unitMeasurement));

        return unitMeasurement;
    }
}
