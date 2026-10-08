using SistemaFacturacion.App.DTOs;

namespace SistemaFacturacion.App.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}
