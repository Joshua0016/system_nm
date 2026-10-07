using SistemaFacturacion.Infrastructure;
using SistemaFacturacion.Application;
using SistemaFacturacion.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Inicializar base de datos con schema.sql
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    var schemaPath = Path.Combine(
        Directory.GetParent(AppContext.BaseDirectory)!
            .Parent!.Parent!.Parent!.FullName,
        "schema.sql"
    );

    if (File.Exists(schemaPath))
    {
        var sql = await File.ReadAllTextAsync(schemaPath);
        await db.Database.ExecuteSqlRawAsync(sql);
    }
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
