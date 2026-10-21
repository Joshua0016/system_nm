using Microsoft.Extensions.DependencyInjection;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.App.Services;

namespace SistemaFacturacion.App;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
