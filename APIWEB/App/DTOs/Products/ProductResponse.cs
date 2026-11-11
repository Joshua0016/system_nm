using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.App.DTOs;

public class ProductResponse
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Reference { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Stock { get; set; }

    public decimal Markup { get; set; }

    public decimal Cost { get; set; }

    public decimal Price { get; set; }

    public UnitMeasurement UnitMeasurement { get; set; }

    public DateTime CreatedAt { get; set; }

    public static ProductResponse FromEntity(Product product) => new()
    {
        Id = product.Id,
        Code = product.Code,
        Reference = product.Reference,
        Description = product.Description,
        Stock = product.Stock,
        Markup = product.Markup,
        Cost = product.Cost,
        Price = product.Price,
        UnitMeasurement = product.UnitMeasurement,
        CreatedAt = product.CreatedAt
    };
}
