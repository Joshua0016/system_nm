using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.App.DTOs;

public class ChangePasswordRequest
{
    public const int PasswordMinLength = 6;

    [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
    [MinLength(PasswordMinLength)]
    public string NewPassword { get; set; } = string.Empty;
}
