using SistemaFacturacion.App.DTOs;
using SistemaFacturacion.App.Exceptions;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.App.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<ProductResponse>> GetAllAsync(string? search)
    {
        var products = await _productRepository.GetAllAsync(search?.Trim());

        return products.Select(ProductResponse.FromEntity).ToList();
    }

    public async Task<ProductResponse> GetByIdAsync(int id)
    {
        var product = await GetProductOrThrowAsync(id);

        return ProductResponse.FromEntity(product);
    }

    public async Task<ProductResponse> GetByCodeAsync(string code)
    {
        var product = await _productRepository.GetByCodeAsync(code.Trim())
            ?? throw new NotFoundException($"No existe un producto con código '{code}'.");

        return ProductResponse.FromEntity(product);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var product = new Product(
            await _productRepository.GetNextCodeAsync(),
            request.Reference,
            request.Description,
            request.Cost!.Value,
            request.Markup!.Value,
            request.Price,
            request.Stock!.Value,
            request.UnitMeasurement!.Value);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return ProductResponse.FromEntity(product);
    }

    public async Task<ProductResponse> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await GetProductOrThrowAsync(id);

        product.Update(
            request.Reference,
            request.Description,
            request.Cost!.Value,
            request.Markup!.Value,
            request.Price,
            request.Stock!.Value,
            request.UnitMeasurement!.Value);

        await _productRepository.SaveChangesAsync();

        return ProductResponse.FromEntity(product);
    }

    private async Task<Product> GetProductOrThrowAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"No existe un producto con id {id}.");
    }
}
