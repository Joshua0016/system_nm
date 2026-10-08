using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class ReturnDetailConfiguration : IEntityTypeConfiguration<ReturnDetail>
{
    public void Configure(EntityTypeBuilder<ReturnDetail> builder)
    {
        builder.ToTable("return_details");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ReturnId)
            .HasColumnName("return_id");

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id");

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.UnitPrice)
            .HasColumnName("unit_price")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TaxRate)
            .HasColumnName("tax_rate")
            .HasColumnType("decimal(5,4)");

        builder.Property(x => x.Subtotal)
            .HasColumnName("subtotal")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TaxAmount)
            .HasColumnName("tax_amount")
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}
