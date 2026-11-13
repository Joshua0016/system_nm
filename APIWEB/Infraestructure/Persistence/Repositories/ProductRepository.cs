using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.App.Exceptions;
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

        // Cada palabra debe aparecer en el código, la referencia o la descripción:
        // "tubo pvc" encuentra "Tubo de PVC". LIKE en SQLite ignora mayúsculas solo en ASCII
        var terms = search?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];

        foreach (var term in terms)
        {
            var pattern = $"%{EscapeLike(term)}%";

            query = query.Where(p =>
                EF.Functions.Like(p.Code, pattern, LikeEscape) ||
                EF.Functions.Like(p.Reference, pattern, LikeEscape) ||
                EF.Functions.Like(p.Description, pattern, LikeEscape));
        }

        return await query
            .OrderBy(p => p.Description)
            .ToListAsync();
    }

    public async Task<string> GetNextCodeAsync()
    {
        // Con ceros a la izquierda el orden de texto coincide con el numérico
        var lastCode = await _context.Products
            .Where(p => p.Code.StartsWith(Product.CodePrefix))
            .MaxAsync(p => (string?)p.Code);

        var lastNumber = lastCode is not null
            && int.TryParse(lastCode[Product.CodePrefix.Length..], out var number)
                ? number
                : 0;

        return $"{Product.CodePrefix}{(lastNumber + 1).ToString($"D{Product.CodeDigits}")}";
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqliteException
        {
            SqliteExtendedErrorCode: SqliteUniqueConstraintError
        })
        {
            // Dos altas simultáneas pueden calcular el mismo código siguiente
            throw new ConflictException("Otro usuario acaba de crear un producto con el mismo código. Intenta de nuevo.");
        }
    }

    private const int SqliteUniqueConstraintError = 2067;

    private const string LikeEscape = "\\";

    private static string EscapeLike(string value)
    {
        return value
            .Replace("\\", "\\\\")
            .Replace("%", "\\%")
            .Replace("_", "\\_");
    }
}
