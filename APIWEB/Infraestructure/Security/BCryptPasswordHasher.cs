using  SistemaFacturacion.App.Interfaces;


namespace SistemaFacturacion.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    public bool veryfy(string password,string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password,passwordHash);
    }

    public string Hash (string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
        
    }
}