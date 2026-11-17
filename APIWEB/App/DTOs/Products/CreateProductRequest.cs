using System.ComponentModel.DataAnnotations;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.App.DTOs;

// El código no se envía: lo genera el sistema (PRD-000001…)
public class CreateProductRequest
{
    [Required(ErrorMessage = "La referencia es obligatoria.")]
    [MaxLength(Product.ReferenceMaxLength)]
    public string Reference { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [MaxLength(Product.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "El costo es obligatorio.")]
    [Range(0.01, 999_999_999, ErrorMessage = "El costo debe ser mayor que cero.")]
    public decimal? Cost { get; set; }

    [Required(ErrorMessage = "El markup es obligatorio.")]
    [Range(0.0001, 999_999_999, ErrorMessage = "El markup debe ser mayor que cero (0.30 = 30 %).")]
    public decimal? Markup { get; set; }

    // Opcional: si no se envía queda en 0 y el precio se indica al facturar
    [Range(0.0, 999_999_999, ErrorMessage = "El precio no puede ser negativo.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Range(0.0, 999_999_999, ErrorMessage = "El stock no puede ser negativo.")]
    public decimal? Stock { get; set; }

    [Required(ErrorMessage = "La unidad de medida es obligatoria.")]
    [EnumDataType(typeof(UnitMeasurement))]
    public UnitMeasurement? UnitMeasurement { get; set; }
}
