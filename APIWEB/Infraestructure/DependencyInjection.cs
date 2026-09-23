using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaFacturacion.Domain.Interfaces;
using SistemaFacturacion.Infrastructure.Persistence;
using SistemaFacturacion.Infrastructure.Persistence.Repositories;

namespace SistemaFacturacion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            ));

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}