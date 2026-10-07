using Entities;

namespace SistemaFacturacion.App.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string username);
}