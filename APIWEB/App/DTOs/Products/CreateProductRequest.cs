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

    // Valores opcionales: si no se envían quedan en 0. La entidad rechaza negativos
    public decimal Cost { get; set; }

    // Utilidad sobre el costo (0.30 = 30 %)
    public decimal Markup { get; set; }

    // El precio final se indica al facturar
    public decimal Price { get; set; }

    public decimal Stock { get; set; }

    [Required(ErrorMessage = "La unidad de medida es obligatoria.")]
    [EnumDataType(typeof(UnitMeasurement))]
    public UnitMeasurement? UnitMeasurement { get; set; }
}
