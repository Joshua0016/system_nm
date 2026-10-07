using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class InvoiceDetailConfiguration
    : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(
        EntityTypeBuilder<InvoiceDetail> builder)
    {
        builder.ToTable("invoice_details");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.InvoiceId)
            .HasColumnName("invoice_id")
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

        // InvoiceDetail -> Invoice
        builder.HasOne(x => x.Invoice)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // InvoiceDetail -> Product
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}