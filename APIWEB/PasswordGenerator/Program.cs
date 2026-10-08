using BCrypt.Net;

Console.Write("Escribe la contraseña: ");

var password = Console.ReadLine();

if (string.IsNullOrWhiteSpace(password))
{
    Console.WriteLine("La contraseña no puede estar vacía.");
    return;
}

var hash = BCrypt.Net.BCrypt.HashPassword(password);

Console.WriteLine();
Console.WriteLine("Hash BCrypt:");
Console.WriteLine(hash);