namespace SistemaFacturacion.Domain.Entities;

using SistemaFacturacion.Domain.Enums;

public class Product
{
    public const int CodeMaxLength = 50;

    // Formato del código autogenerado: PRD-000001, PRD-000002…
    public const string CodePrefix = "PRD-";
    public const int CodeDigits = 6;
    public const int ReferenceMaxLength = 100;
    public const int DescriptionMaxLength = 200;

    public int Id { get; private set; }

    public string Code { get; private set; } = string.Empty;

    public string Reference { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Stock { get; private set; }

    // Utilidad sobre el costo que fija el usuario (0.30 = 30 %); no se calcula
    public decimal Markup { get; private set; }

    public decimal Cost { get; private set; }

    // Puede quedar en 0: el precio final se indica al facturar
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
        decimal cost,
        decimal markup,
        decimal price,
        decimal stock,
        UnitMeasurement unitMeasurement)
    {
        Code = ValidateText(code, CodeMaxLength, "El código");
        CreatedAt = DateTime.UtcNow;
        Update(reference, description, cost, markup, price, stock, unitMeasurement);
    }

    // El código lo genera el sistema al crear y no se modifica
    public void Update(
        string reference,
        string description,
        decimal cost,
        decimal markup,
        decimal price,
        decimal stock,
        UnitMeasurement unitMeasurement)
    {
        Reference = ValidateText(reference, ReferenceMaxLength, "La referencia");
        Description = ValidateText(description, DescriptionMaxLength, "La descripción");
        UnitMeasurement = ValidateUnitMeasurement(unitMeasurement);
        Stock = ValidateStock(stock, UnitMeasurement);
        Cost = ValidateCost(cost);
        Markup = ValidateMarkup(markup);
        Price = ValidatePrice(price);
    }

    // Lo usarán las compras al dar entrada a mercancía; no toca markup ni precio
    public void UpdateCost(decimal cost)
    {
        Cost = ValidateCost(cost);
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

    private static decimal ValidateCost(decimal cost)
    {
        if (cost < 0)
            throw new ArgumentException("El costo no puede ser negativo.", nameof(cost));

        return Math.Round(cost, 2, MidpointRounding.AwayFromZero);
    }

    private static decimal ValidateMarkup(decimal markup)
    {
        if (markup < 0)
            throw new ArgumentException("El markup no puede ser negativo.", nameof(markup));

        return Math.Round(markup, 4, MidpointRounding.AwayFromZero);
    }

    private static decimal ValidatePrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("El precio no puede ser negativo.", nameof(price));

        return Math.Round(price, 2, MidpointRounding.AwayFromZero);
    }

    private static decimal ValidateStock(decimal stock, UnitMeasurement unitMeasurement)
    {
        if (stock < 0)
            throw new ArgumentException("El stock no puede ser negativo.", nameof(stock));

        if (unitMeasurement == UnitMeasurement.Unidad && stock != decimal.Truncate(stock))
            throw new ArgumentException("Los productos por unidad solo admiten stock entero.", nameof(stock));

        return stock;
    }

    private static UnitMeasurement ValidateUnitMeasurement(UnitMeasurement unitMeasurement)
    {
        if (!Enum.IsDefined(unitMeasurement))
            throw new ArgumentException("Unidad de medida no válida.", nameof(unitMeasurement));

        return unitMeasurement;
    }
}
