using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.App.DTOs;

public class UserResponse
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public static UserResponse FromEntity(User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        FullName = user.FullName,
        Role = user.Role,
        IsActive = user.IsActive,
        CreatedAt = user.CreatedAt
    };
}
