namespace Technico.Dtos;
using Technico.Models;

public class UserDTO
{
    public Guid Id { get; set; }
    public string VATNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public User.Type UserType { get; set; }

    public UserDTO() { }

    public UserDTO(Guid id, string vatNumber, string name, string surname, string address, string phoneNumber, string email, string password, User.Type type)
    {
        Id = id;
        VATNumber = vatNumber;
        Name = name;
        Surname = surname;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        UserType = type;
    }
}