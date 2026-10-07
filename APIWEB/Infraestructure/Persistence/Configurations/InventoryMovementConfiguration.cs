using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;
using Enums;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class InventoryMovementConfiguration
    : IEntityTypeConfiguration<InventoryMovement>
{
    public void Configure(
        EntityTypeBuilder<InventoryMovement> builder)
    {
        builder.ToTable("inventory_movements");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnName("type")
            .HasConversion(
                value => value.ToString(),
                value => Enum.Parse<Enums.InventoryMovementType>(value, true))
            .IsRequired();

        builder.Property(x => x.Concept)
            .HasColumnName("concept")
            .HasConversion(
                value => ConceptToString(value),
                value => StringToConcept(value))
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(x => x.StockBefore)
            .HasColumnName("stock_before")
            .IsRequired();

        builder.Property(x => x.StockAfter)
            .HasColumnName("stock_after")
            .IsRequired();

        builder.Property(x => x.ReferenceInvoiceId)
            .HasColumnName("reference_invoice_id");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        // Product -> InventoryMovements
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // User -> InventoryMovements
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static string ConceptToString(Enums.InventoryMovementConcept value) => value switch
    {
        Enums.InventoryMovementConcept.Purchase      => "purchase",
        Enums.InventoryMovementConcept.Sale          => "sale",
        Enums.InventoryMovementConcept.AdjustmentIn  => "adjustment_in",
        Enums.InventoryMovementConcept.AdjustmentOut => "adjustment_out",
        Enums.InventoryMovementConcept.Return        => "return",
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };

    private static Enums.InventoryMovementConcept StringToConcept(string value) => value switch
    {
        "purchase"       => Enums.InventoryMovementConcept.Purchase,
        "sale"           => Enums.InventoryMovementConcept.Sale,
        "adjustment_in"  => Enums.InventoryMovementConcept.AdjustmentIn,
        "adjustment_out" => Enums.InventoryMovementConcept.AdjustmentOut,
        "return"         => Enums.InventoryMovementConcept.Return,
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };
}