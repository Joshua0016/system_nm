using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.App.DTOs;

namespace SistemaFacturacion.App.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username.Trim());

        if (user is null)
        {
            return null;
        }

        if (!user.IsActive)
        {
            return null;
        }

        var passwordIsValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!passwordIsValid)
        {
            return null;
        }

        var (token, expiresAt) = _jwtTokenGenerator.Generate(user);

        return new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role,
            Token = token,
            ExpiresAt = expiresAt
        };
    }
}
