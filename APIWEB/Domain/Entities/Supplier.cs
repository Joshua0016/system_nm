namespace Entities;

public class Supplier
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public string? Email { get; private set; }

    private Supplier()
    {
    }

    public Supplier(
        string name,
        string address,
        string phoneNumber,
        string? email)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}