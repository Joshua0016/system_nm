namespace Entities;

public class Client
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? TaxId { get; private set; }

    public string? Address { get; private set; }

    public string? City { get; private set; }

    public string? PhoneNumber { get; private set; }

    public string? Email { get; private set; }

    public decimal CreditLimit { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Client()
    {
    }

    public Client(
        string name,
        string? taxId,
        string? address,
        string? city,
        string? phoneNumber,
        string? email,
        decimal creditLimit)
    {
        if (creditLimit < 0)
            throw new ArgumentException(
                "El límite de crédito no puede ser negativo.");

        Name = name;
        TaxId = taxId;
        Address = address;
        City = city;
        PhoneNumber = phoneNumber;
        Email = email;
        CreditLimit = creditLimit;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}
