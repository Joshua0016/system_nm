namespace SistemaFacturacion.Domain.Entities;

using SistemaFacturacion.Domain.Enums;

public class User
{
    public const int UsernameMaxLength = 50;
    public const int FullNameMaxLength = 100;

    public int Id { get; private set; }

    public string Username { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string FullName { get; private set; } = string.Empty;

    public UserRole Role { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private User()
    {
    }

    public User(
        string username,
        string passwordHash,
        string fullName,
        UserRole role)
    {
        Username = ValidateUsername(username);
        PasswordHash = ValidatePasswordHash(passwordHash);
        FullName = ValidateFullName(fullName);
        Role = ValidateRole(role);
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateFullName(string fullName)
    {
        FullName = ValidateFullName(fullName);
    }

    public void ChangeRole(UserRole role)
    {
        Role = ValidateRole(role);
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = ValidatePasswordHash(passwordHash);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static string ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("El nombre de usuario es obligatorio.", nameof(username));

        username = username.Trim();

        if (username.Length > UsernameMaxLength)
            throw new ArgumentException($"El nombre de usuario no puede superar {UsernameMaxLength} caracteres.", nameof(username));

        return username;
    }

    private static string ValidateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("El nombre completo es obligatorio.", nameof(fullName));

        fullName = fullName.Trim();

        if (fullName.Length > FullNameMaxLength)
            throw new ArgumentException($"El nombre completo no puede superar {FullNameMaxLength} caracteres.", nameof(fullName));

        return fullName;
    }

    private static string ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("El hash de la contraseña es obligatorio.", nameof(passwordHash));

        return passwordHash;
    }

    private static UserRole ValidateRole(UserRole role)
    {
        if (!Enum.IsDefined(role))
            throw new ArgumentException("Rol de usuario no válido.", nameof(role));

        return role;
    }
}
