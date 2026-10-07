namespace Entities;

public class User
{
    public int Id { get; private set; }

    public string Username { get; private set; }

    public string PasswordHash { get; private set; }

    public string FullName { get; private set; }

    public string Role { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public User(
        string username,
        string passwordHash,
        string fullName,
        string role)
    {
        Username = username;
        PasswordHash = passwordHash;
        FullName = fullName;
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}