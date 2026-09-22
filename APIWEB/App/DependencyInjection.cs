
using System;

using Microsoft.Extensions.DependencyInjection;
using SistemaFacturacion.Application.UseCases.Customers.CreateCustomer;

namespace SistemaFacturacion.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateCustomerUseCase>();

        return services;
    }
}