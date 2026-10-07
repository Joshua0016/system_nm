using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class PurchaseDetailConfiguration
    : IEntityTypeConfiguration<PurchaseDetail>
{
    public void Configure(
        EntityTypeBuilder<PurchaseDetail> builder)
    {
        builder.ToTable("purchase_details");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PurchaseId)
            .HasColumnName("purchase_id")
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(x => x.UnitCost)
            .HasColumnName("unit_cost")
            .IsRequired();

        builder.Property(x => x.TaxIncluded)
            .HasColumnName("tax_included")
            .HasDefaultValue(false);

        builder.Property(x => x.TaxRate)
            .HasColumnName("tax_rate")
            .HasDefaultValue(0.18m);

        // PurchaseDetail -> Purchase
        builder.HasOne(x => x.Purchase)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.PurchaseId)
            .OnDelete(DeleteBehavior.Cascade);

        // PurchaseDetail -> Product
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
