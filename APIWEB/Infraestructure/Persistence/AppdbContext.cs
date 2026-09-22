using Microsoft.EntityFrameworkCore;

using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
}