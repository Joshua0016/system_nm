namespace Entities;

using Enums;


public class Product
{
    public int Id { get; private set; }

    public string Code { get; private set; }

    public string Reference { get; private set; }

    public string Description { get; private set; }

    public decimal Stock { get; private set; }

    public decimal Markup { get; private set; }

    public decimal Cost { get; private set; }

    public decimal Price { get; private set; }

    public UnitMeasurement UnitMeasurement { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Product(
        string code,
        string reference,
        string description,
        decimal markup)
    {
        Code = code;
        Reference = reference;
        Description = description;

        if (markup <= 0)
            throw new ArgumentException(
                "El markup debe ser mayor que cero.");

        Markup = markup;

        Stock = 0;
        Cost = 0;
        Price = 0;
        CreatedAt = DateTime.UtcNow;
    }
}