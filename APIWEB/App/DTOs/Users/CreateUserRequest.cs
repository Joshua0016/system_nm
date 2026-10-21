using System.ComponentModel.DataAnnotations;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.App.DTOs;

public class CreateUserRequest
{
    [Required(ErrorMessage = "El usuario es obligatorio.")]
    [MaxLength(User.UsernameMaxLength)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(ChangePasswordRequest.PasswordMinLength)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [MaxLength(User.FullNameMaxLength)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    [EnumDataType(typeof(UserRole))]
    public UserRole? Role { get; set; }
}
