using Entities;

namespace SistemaFacturacion.App.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
}
