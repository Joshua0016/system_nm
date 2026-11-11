using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.App.Interfaces;

namespace SistemaFacturacion.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product?> GetByCodeAsync(string code)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(string? search)
    {
        var query = _context.Products.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            // LIKE en SQLite ya ignora mayúsculas/minúsculas en ASCII
            var pattern = $"%{search}%";

            query = query.Where(p =>
                EF.Functions.Like(p.Code, pattern) ||
                EF.Functions.Like(p.Reference, pattern) ||
                EF.Functions.Like(p.Description, pattern));
        }

        return await query
            .OrderBy(p => p.Description)
            .ToListAsync();
    }

    public async Task<bool> ExistsByCodeAsync(string code, int? excludeId = null)
    {
        return await _context.Products
            .AnyAsync(p => p.Code == code && (excludeId == null || p.Id != excludeId));
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
