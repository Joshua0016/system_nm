using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;
using Enums;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(
        EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Reference)
            .HasColumnName("reference")
            .IsRequired();

        builder.HasIndex(x => x.Reference)
            .IsUnique();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(x => x.Stock)
            .HasColumnName("stock")
            .HasDefaultValue(0);

        builder.Property(x => x.Markup)
            .HasColumnName("markup")
            .IsRequired();

        builder.Property(x => x.Cost)
            .HasColumnName("cost")
            .HasDefaultValue(0);

        builder.Property(x => x.Price)
            .HasColumnName("price")
            .HasDefaultValue(0);

        builder.Property(x => x.UnitMeasurement)
            .HasColumnName("unit_measurement")
            .HasConversion(
                value => value.ToString().ToLower(),
                value => Enum.Parse<UnitMeasurement>(
                    value,
                    true))
            .HasDefaultValue(UnitMeasurement.Pulgada);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
    }
}