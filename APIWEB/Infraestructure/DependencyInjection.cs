using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaFacturacion.Infrastructure.Persistence;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.Infrastructure.Persistence.Repositories;
using SistemaFacturacion.Infrastructure.Security;

namespace SistemaFacturacion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")
            ));

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
