namespace SistemaFacturacion.App.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(string username, string password);
}
