using SistemaFacturacion.App.DTOs;

namespace SistemaFacturacion.App.Interfaces;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponse>> GetAllAsync(string? search);

    Task<ProductResponse> GetByIdAsync(int id);

    Task<ProductResponse> GetByCodeAsync(string code);

    Task<ProductResponse> CreateAsync(CreateProductRequest request);

    Task<ProductResponse> UpdateAsync(int id, UpdateProductRequest request);
}
