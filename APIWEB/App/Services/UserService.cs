using SistemaFacturacion.App.DTOs;
using SistemaFacturacion.App.Exceptions;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.App.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<IReadOnlyList<UserResponse>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(UserResponse.FromEntity).ToList();
    }

    public async Task<UserResponse> GetByIdAsync(int id)
    {
        var user = await GetUserOrThrowAsync(id);

        return UserResponse.FromEntity(user);
    }

    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        var username = request.Username.Trim();

        if (await _userRepository.ExistsByUsernameAsync(username))
        {
            throw new ConflictException($"El usuario '{username}' ya existe.");
        }

        var user = new User(
            username,
            _passwordHasher.Hash(request.Password),
            request.FullName,
            request.Role!.Value);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return UserResponse.FromEntity(user);
    }

    public async Task<UserResponse> UpdateAsync(int id, UpdateUserRequest request)
    {
        var user = await GetUserOrThrowAsync(id);

        user.UpdateFullName(request.FullName);
        user.ChangeRole(request.Role!.Value);

        await _userRepository.SaveChangesAsync();

        return UserResponse.FromEntity(user);
    }

    public async Task ChangePasswordAsync(int id, ChangePasswordRequest request)
    {
        var user = await GetUserOrThrowAsync(id);

        user.ChangePassword(_passwordHasher.Hash(request.NewPassword));

        await _userRepository.SaveChangesAsync();
    }

    public async Task ActivateAsync(int id)
    {
        var user = await GetUserOrThrowAsync(id);

        user.Activate();

        await _userRepository.SaveChangesAsync();
    }

    public async Task DeactivateAsync(int id, int currentUserId)
    {
        if (id == currentUserId)
        {
            throw new ArgumentException("No puedes desactivar tu propio usuario.");
        }

        var user = await GetUserOrThrowAsync(id);

        user.Deactivate();

        await _userRepository.SaveChangesAsync();
    }

    private async Task<User> GetUserOrThrowAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"No existe un usuario con id {id}.");
    }
}
