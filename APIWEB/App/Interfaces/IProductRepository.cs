using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.App.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);

    Task<Product?> GetByCodeAsync(string code);

    Task<IReadOnlyList<Product>> GetAllAsync(string? search);

    Task<bool> ExistsByCodeAsync(string code, int? excludeId = null);

    Task AddAsync(Product product);

    Task SaveChangesAsync();
}
