using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Infrastructure.Persistence;
namespace SistemaFacturacion.API.Controllers;



[ApiController]
[Route("api/test")]

public class TestController : ControllerBase
{

    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {

        _context = context;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {

        var products = await _context.Products
            .ToListAsync();

        return Ok(products);
    }
}