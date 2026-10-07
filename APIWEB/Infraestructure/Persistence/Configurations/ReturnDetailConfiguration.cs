using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class ReturnDetailConfiguration
    : IEntityTypeConfiguration<ReturnDetail>
{
    public void Configure(
        EntityTypeBuilder<ReturnDetail> builder)
    {
        builder.ToTable("return_details");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ReturnId)
            .HasColumnName("return_id")
            .IsRequired();

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasColumnName("unit_price")
            .IsRequired();

        builder.Property(x => x.TaxRate)
            .HasColumnName("tax_rate")
            .HasDefaultValue(0.18m);

        builder.Property(x => x.Subtotal)
            .HasColumnName("subtotal")
            .IsRequired();

        builder.Property(x => x.TaxAmount)
            .HasColumnName("tax_amount")
            .IsRequired();

        // ReturnDetail -> Return
        builder.HasOne(x => x.Return)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.ReturnId)
            .OnDelete(DeleteBehavior.Cascade);

        // ReturnDetail -> Product
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}