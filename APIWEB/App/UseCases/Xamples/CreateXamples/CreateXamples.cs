using SistemaFacturacion.Application.DTOs.Customers;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Interfaces;

namespace SistemaFacturacion.Application.UseCases.Customers.CreateCustomer;

public class CreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerUseCase(
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> ExecuteAsync(
        CreateCustomerRequest request)
    {
        var customer = new Customer(
            request.Name,
            request.Email
        );

        return await _customerRepository
            .CreateAsync(customer);
    }
}