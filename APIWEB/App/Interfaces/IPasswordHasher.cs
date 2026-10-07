namespace SistemaFacturacion.App.Interfaces;

public interface IPasswordHasher
{
    bool veryfy(string password, string passwordHash);

    string Hash(string password);
    
}