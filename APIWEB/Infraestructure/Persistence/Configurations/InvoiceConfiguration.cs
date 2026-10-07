using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;
using Enums;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration
    : IEntityTypeConfiguration<Invoice>
{
    public void Configure(
        EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ClientId)
            .HasColumnName("client_id");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.ClientName)
            .HasColumnName("client_name");

        builder.Property(x => x.ClientTaxId)
            .HasColumnName("client_tax_id");

        builder.Property(x => x.InvoiceNumber)
            .HasColumnName("invoice_number");

        builder.HasIndex(x => x.InvoiceNumber)
            .IsUnique();

        builder.Property(x => x.PaymentType)
            .HasColumnName("payment_type")
            .HasConversion(
                value => value.ToString().ToLower(),
                value => Enum.Parse<Enums.PaymentType>(
                    value,
                    true))
            .HasDefaultValue(Enums.PaymentType.Cash);

        builder.Property(x => x.Subtotal)
            .HasColumnName("subtotal")
            .HasDefaultValue(0);

        builder.Property(x => x.TaxAmount)
            .HasColumnName("tax_amount")
            .HasDefaultValue(0);

        builder.Property(x => x.DiscountAmount)
            .HasColumnName("discount_amount")
            .HasDefaultValue(0);

        builder.Property(x => x.TotalAmount)
            .HasColumnName("total_amount")
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        // Invoice -> Client
        builder.HasOne(x => x.Client)
            .WithMany()
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Invoice -> User
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Invoice -> InvoiceDetails
        builder.HasMany(x => x.Details)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}