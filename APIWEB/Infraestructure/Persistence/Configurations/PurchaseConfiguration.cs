using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class PurchaseConfiguration
    : IEntityTypeConfiguration<Purchase>
{
    public void Configure(
        EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("purchases");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.InvoiceNumber)
            .HasColumnName("invoice_number")
            .IsRequired();

        builder.Property(x => x.SupplierId)
            .HasColumnName("supplier_id")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        // Purchase -> Supplier
        builder.HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Purchase -> User
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Purchase -> PurchaseDetails
        builder.HasMany(x => x.Details)
            .WithOne(x => x.Purchase)
            .HasForeignKey(x => x.PurchaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}