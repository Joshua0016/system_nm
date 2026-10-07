using Microsoft.EntityFrameworkCore;
using Entities;
using SistemaFacturacion.App.Interfaces;
using SistemaFacturacion.Infrastructure.Persistence;

namespace SistemaFacturacion.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}