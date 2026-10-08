using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
{
    public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
    {
        builder.ToTable("purchase_details");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PurchaseId)
            .HasColumnName("purchase_id");

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id");

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.UnitCost)
            .HasColumnName("unit_cost")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TaxIncluded)
            .HasColumnName("tax_included");

        builder.Property(x => x.TaxRate)
            .HasColumnName("tax_rate")
            .HasColumnType("decimal(5,4)");

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}
