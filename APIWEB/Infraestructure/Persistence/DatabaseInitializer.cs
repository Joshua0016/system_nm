using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SistemaFacturacion.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    // schema.sql va embebido en el ensamblado, así funciona igual con 'dotnet run' que publicado
    private const string SchemaResourceName = "SistemaFacturacion.Infrastructure.schema.sql";

    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await using var stream = typeof(DatabaseInitializer).Assembly
            .GetManifestResourceStream(SchemaResourceName)
            ?? throw new InvalidOperationException($"No se encontró el recurso embebido '{SchemaResourceName}'.");

        using var reader = new StreamReader(stream);
        var sql = await reader.ReadToEndAsync();

        await db.Database.ExecuteSqlRawAsync(sql);
    }
}
