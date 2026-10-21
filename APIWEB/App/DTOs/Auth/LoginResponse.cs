using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.App.DTOs;

public class LoginResponse
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }
}
