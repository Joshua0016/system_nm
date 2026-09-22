using SistemaFacturacion.Domain.Entities;

namespace SistemaFacturacion.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> CreateAsync(Customer customer);

    Task<Customer?> GetByIdAsync(int id);

    Task<IEnumerable<Customer>> GetAllAsync();
}