using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class InventoryMovementConfiguration : IEntityTypeConfiguration<InventoryMovement>
{
    public void Configure(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder.ToTable("inventory_movements");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id");

        builder.Property(x => x.Type)
            .HasColumnName("type")
            .HasConversion<string>();

        builder.Property(x => x.Concept)
            .HasColumnName("concept")
            .HasConversion<string>();

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.StockBefore)
            .HasColumnName("stock_before")
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.StockAfter)
            .HasColumnName("stock_after")
            .HasColumnType("decimal(18,4)");

        builder.Property(x => x.ReferenceInvoiceId)
            .HasColumnName("reference_invoice_id");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
