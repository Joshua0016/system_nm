using SistemaFacturacion.App.DTOs.Auth;


namespace SistemaFacturacion.App.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}