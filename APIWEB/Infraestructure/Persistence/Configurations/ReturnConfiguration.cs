using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class ReturnConfiguration : IEntityTypeConfiguration<Return>
{
    public void Configure(EntityTypeBuilder<Return> builder)
    {
        builder.ToTable("returns");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.InvoiceId)
            .HasColumnName("invoice_id");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id");

        builder.Property(x => x.Ncf)
            .HasColumnName("ncf")
            .IsRequired();

        builder.Property(x => x.AffectedNcf)
            .HasColumnName("affected_ncf")
            .IsRequired();

        builder.Property(x => x.ReasonCode)
            .HasColumnName("reason_code")
            .HasConversion<string>();

        builder.Property(x => x.Subtotal)
            .HasColumnName("subtotal")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TaxAmount)
            .HasColumnName("tax_amount")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Total)
            .HasColumnName("total")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey(x => x.InvoiceId);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Details)
            .WithOne(x => x.Return)
            .HasForeignKey(x => x.ReturnId);
    }
}
