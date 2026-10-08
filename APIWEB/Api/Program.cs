using SistemaFacturacion.Infrastructure;
using SistemaFacturacion.Application;
using SistemaFacturacion.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Resolver la ruta del .db relativa al proyecto (no al bin/)
var projectDir = Directory.GetParent(AppContext.BaseDirectory)!
    .Parent!.Parent!.Parent!.FullName;

var dbPath = Path.Combine(projectDir, "Data", "SistemaFacturacion.db");

// Sobrescribir la connection string con la ruta absoluta
builder.Configuration["ConnectionStrings:DefaultConnection"] = $"Data Source={dbPath}";

// Services
builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar schema.sql si la base de datos no tiene tablas
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var schemaPath = Path.Combine(
        Directory.GetParent(projectDir)!.FullName,
        "Infraestructure",
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
