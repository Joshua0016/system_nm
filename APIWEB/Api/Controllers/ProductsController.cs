using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.App.DTOs;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.Api.Controllers;

[ApiController]
[Route("api/products")]
[Produces("application/json")]
[Authorize]
public class ProductsController : ControllerBase
{
    private const string CanManageProducts = nameof(UserRole.Admin) + "," + nameof(UserRole.Supervisor);

    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // Filtra por código, referencia o descripción
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAll([FromQuery] string? search)
    {
        return Ok(await _productService.GetAllAsync(search));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetById(int id)
    {
        return Ok(await _productService.GetByIdAsync(id));
    }

    [HttpGet("by-code/{code}")]
    public async Task<ActionResult<ProductResponse>> GetByCode(string code)
    {
        return Ok(await _productService.GetByCodeAsync(code));
    }

    [HttpPost]
    [Authorize(Roles = CanManageProducts)]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request)
    {
        var product = await _productService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = CanManageProducts)]
    public async Task<ActionResult<ProductResponse>> Update(int id, [FromBody] UpdateProductRequest request)
    {
        return Ok(await _productService.UpdateAsync(id, request));
    }
}
