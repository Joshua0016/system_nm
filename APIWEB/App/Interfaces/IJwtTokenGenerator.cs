using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.App.Interfaces;

public interface IJwtTokenGenerator
{
    (string Token, DateTime ExpiresAt) Generate(User user);
}
