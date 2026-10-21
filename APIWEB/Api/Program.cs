using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SistemaFacturacion.Api.ExceptionHandling;
using SistemaFacturacion.App;
using SistemaFacturacion.Infrastructure;
using SistemaFacturacion.Infrastructure.Persistence;
using SistemaFacturacion.Infrastructure.Security;

const string CorsPolicyName = "Frontend";

var builder = WebApplication.CreateBuilder(args);

// Una ruta relativa del .db se resuelve contra el ContentRoot
// (carpeta del proyecto con 'dotnet run', carpeta de la app cuando se publica)
var sqlite = new SqliteConnectionStringBuilder(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Falta 'ConnectionStrings:DefaultConnection'."));

if (!Path.IsPathRooted(sqlite.DataSource))
{
    sqlite.DataSource = Path.Combine(builder.Environment.ContentRootPath, sqlite.DataSource);
}

Directory.CreateDirectory(Path.GetDirectoryName(sqlite.DataSource)!);
builder.Configuration["ConnectionStrings:DefaultConnection"] = sqlite.ToString();

var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
    ?? new JwtSettings();

if (jwtSettings.Key.Length < 32)
{
    throw new InvalidOperationException(
        "Configura 'Jwt:Key' con al menos 32 caracteres (variable de entorno Jwt__Key fuera de Development).");
}

// Services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<AppExceptionHandler>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
            NameClaimType = JwtRegisteredClaimNames.UniqueName,
            RoleClaimType = JwtTokenGenerator.RoleClaimType
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
    options.AddPolicy(CorsPolicyName, policy =>
        policy.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pega el token devuelto por POST /api/auth/login"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Crea las tablas (CREATE TABLE IF NOT EXISTS) y el admin inicial si no existen
await DatabaseInitializer.InitializeAsync(app.Services);

// Pipeline
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CorsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
