using System.ComponentModel.DataAnnotations;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.App.DTOs;

public class UpdateUserRequest
{
    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [MaxLength(User.FullNameMaxLength)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    [EnumDataType(typeof(UserRole))]
    public UserRole? Role { get; set; }

    public string Username { get; set; } = string.Empty;
}
