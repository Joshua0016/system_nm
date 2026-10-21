namespace SistemaFacturacion.Infrastructure.Security;

public class JwtSettings
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    // Mínimo 32 caracteres (HMAC-SHA256). Fuera de Development se pasa por variable de entorno Jwt__Key
    public string Key { get; set; } = string.Empty;

    public int ExpirationMinutes { get; set; } = 480;
}
