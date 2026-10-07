using Microsoft.Extensions.DependencyInjection;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.App.Services;

namespace SistemaFacturacion.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService,AuthService>();

        return services;
    }
}
