using BCrypt.Net;
using SistemaFacturacion.App.Interfaces;

namespace SistemaFacturacion.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return global::BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string passwordHash)
    {
        return global::BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
