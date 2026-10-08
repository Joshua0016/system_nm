using SistemaFacturacion.App.DTOs;
using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.App.Interfaces;

namespace SistemaFacturacion.Api.Controller;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request
    )
    {

        var response = await _authService.LoginAsync(request);

        if (response is null)
        {
            return Unauthorized(new
            {
                message = "usuario o contraseña incorrectos."
            });
        }

        return Ok(response);
    }
}