using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;
using Enums;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class ReturnConfiguration
    : IEntityTypeConfiguration<Return>
{
    public void Configure(
        EntityTypeBuilder<Return> builder)
    {
        builder.ToTable("returns");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.InvoiceId)
            .HasColumnName("invoice_id")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.Ncf)
            .HasColumnName("ncf")
            .IsRequired();

        builder.HasIndex(x => x.Ncf)
            .IsUnique();

        builder.Property(x => x.AffectedNcf)
            .HasColumnName("affected_ncf")
            .IsRequired();

        builder.Property(x => x.ReasonCode)
            .HasColumnName("reason_code")
            .HasConversion(
                value => ReasonCodeToString(value),
                value => StringToReasonCode(value))
            .IsRequired();

        builder.Property(x => x.Subtotal)
            .HasColumnName("subtotal")
            .IsRequired();

        builder.Property(x => x.TaxAmount)
            .HasColumnName("tax_amount")
            .IsRequired();

        builder.Property(x => x.Total)
            .HasColumnName("total")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        // Return -> Invoice
        builder.HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Return -> User
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Return -> ReturnDetails
        builder.HasMany(x => x.Details)
            .WithOne(x => x.Return)
            .HasForeignKey(x => x.ReturnId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static string ReasonCodeToString(ReturnReasonCode value) => value switch
    {
        ReturnReasonCode.Code01 => "01",
        ReturnReasonCode.Code02 => "02",
        ReturnReasonCode.Code03 => "03",
        ReturnReasonCode.Code04 => "04",
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };

    private static ReturnReasonCode StringToReasonCode(string value) => value switch
    {
        "01" => ReturnReasonCode.Code01,
        "02" => ReturnReasonCode.Code02,
        "03" => ReturnReasonCode.Code03,
        "04" => ReturnReasonCode.Code04,
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };
}