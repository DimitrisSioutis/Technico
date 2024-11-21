using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Technico.Models;

[Index(nameof(VATNumber), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(9)]
    public string VATNumber { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Surname { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    [Required]
    [Phone]
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Required]
    public User.Type Role { get; set; } = Type.Client;

    public virtual List<Property> Properties { get; set; } = new List<Property>();

    public enum Type
    {
        Client,
        Admin
    }

    public User() { }
    public User(Guid id, string vatNumber, string name, string surname, string address, string phoneNumber,string email,string password,User.Type role)
    {
        Id = id;
        VATNumber = vatNumber;
        Name = name;
        Surname = surname;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Role = role;
    }
}