using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .HasMaxLength(Product.CodeMaxLength)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Reference)
            .HasColumnName("reference")
            .HasMaxLength(Product.ReferenceMaxLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(Product.DescriptionMaxLength)
            .IsRequired();

        builder.Property(x => x.Stock)
            .HasColumnName("stock")
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.Markup)
            .HasColumnName("markup")
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.Cost)
            .HasColumnName("cost")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(18,2)");

        // schema.sql exige 'PULGADA' | 'CENTIMETROS' | 'MILIMETROS' | 'METROS' (CHECK constraint)
        builder.Property(x => x.UnitMeasurement)
            .HasColumnName("unit_measurement")
            .HasConversion(
                unit => unit.ToString().ToUpperInvariant(),
                value => Enum.Parse<UnitMeasurement>(value, true))
            .IsRequired();

        // SQLite guarda texto sin zona horaria; se marca como UTC al leer
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasConversion(
                value => value,
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
            .IsRequired();
    }
}
