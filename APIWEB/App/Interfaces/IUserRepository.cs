using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.App.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);

    Task<User?> GetByUsernameAsync(string username);

    Task<IReadOnlyList<User>> GetAllAsync();

    Task<bool> ExistsByUsernameAsync(string username);

    Task AddAsync(User user);

    Task SaveChangesAsync();
}
