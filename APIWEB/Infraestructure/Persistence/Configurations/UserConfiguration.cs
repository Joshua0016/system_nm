using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Username)
            .HasColumnName("username")
            .HasMaxLength(User.UsernameMaxLength)
            .IsRequired();

        builder.HasIndex(x => x.Username)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(x => x.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(User.FullNameMaxLength)
            .IsRequired();

        // schema.sql exige 'ADMIN' | 'CASHIER' | 'SUPERVISOR' (CHECK constraint)
        builder.Property(x => x.Role)
            .HasColumnName("role")
            .HasConversion(
                role => role.ToString().ToUpperInvariant(),
                value => Enum.Parse<UserRole>(value, true))
            .IsRequired();

        // Sin HasDefaultValue: con un bool, EF omitiría 'false' en el INSERT y la BD pondría 1
        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        // SQLite guarda texto sin zona horaria; se marca como UTC al leer
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasConversion(
                value => value,
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
            .IsRequired();
    }
}
