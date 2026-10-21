using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.App.DTOs;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.Domain.Enums;

namespace SistemaFacturacion.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserResponse>>> GetAll()
    {
        return Ok(await _userService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse>> GetById(int id)
    {
        return Ok(await _userService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Create([FromBody] CreateUserRequest request)
    {
        var user = await _userService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserResponse>> Update(int id, [FromBody] UpdateUserRequest request)
    {
        return Ok(await _userService.UpdateAsync(id, request));
    }

    [HttpPut("{id:int}/password")]
    public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest request)
    {
        await _userService.ChangePasswordAsync(id, request);

        return NoContent();
    }

    [HttpPut("{id:int}/activate")]
    public async Task<IActionResult> Activate(int id)
    {
        await _userService.ActivateAsync(id);

        return NoContent();
    }

    [HttpPut("{id:int}/deactivate")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var currentUserId = int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value);

        await _userService.DeactivateAsync(id, currentUserId);

        return NoContent();
    }
}
