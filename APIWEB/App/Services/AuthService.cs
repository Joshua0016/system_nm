using Microsoft.VisualBasic;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.App.DTOs;

namespace SistemaFacturacion.App.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _UserRepository;
    private readonly IPasswordHasher _PasswordHasher;

    public AuthService (IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _PasswordHasher = passwordHasher;
        _UserRepository = userRepository;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _UserRepository.GetUserByUsernameAsync(request.Username);

        if (user is null)
        {
            return null;
        }

        if (!user.IsActive)
        {
            return null;
        }

        var passwordIsValid = _PasswordHasher.Verify(request.Password,user.PasswordHash);

        if (!passwordIsValid)
        {
            return null;
        }

        return new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role.ToString(),
            Token = string.Empty
        };
    }
}