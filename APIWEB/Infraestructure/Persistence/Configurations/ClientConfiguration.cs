using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(x => x.TaxId)
            .HasColumnName("tax_id");

        builder.Property(x => x.Address)
            .HasColumnName("address");

        builder.Property(x => x.City)
            .HasColumnName("city");

        builder.Property(x => x.PhoneNumber)
            .HasColumnName("phone_number");

        builder.Property(x => x.Email)
            .HasColumnName("email");

        builder.Property(x => x.CreditLimit)
            .HasColumnName("credit_limit")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
    }
}
