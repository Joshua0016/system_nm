using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.App.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);

    Task<Product?> GetByCodeAsync(string code);

    Task<IReadOnlyList<Product>> GetAllAsync(string? search);

    Task<string> GetNextCodeAsync();

    Task AddAsync(Product product);

    Task SaveChangesAsync();
}
