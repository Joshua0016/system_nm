using SistemaFacturacion.App.DTOs;

namespace SistemaFacturacion.App.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserResponse>> GetAllAsync();

    Task<UserResponse> GetByIdAsync(int id);

    Task<UserResponse> CreateAsync(CreateUserRequest request);

    Task<UserResponse> UpdateAsync(int id, UpdateUserRequest request);

    Task ChangePasswordAsync(int id, ChangePasswordRequest request);

    Task ActivateAsync(int id);

    Task DeactivateAsync(int id, int currentUserId);
}
