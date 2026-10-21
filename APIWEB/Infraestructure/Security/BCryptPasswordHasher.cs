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
        try
        {
            return global::BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch (SaltParseException)
        {
            // Hash mal formado en la BD: se trata como credencial inválida, no como error 500
            return false;
        }
    }
}
