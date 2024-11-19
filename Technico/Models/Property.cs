using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Technico.Models;

[Index(nameof(Address), IsUnique = true)]
public class Property
{
    [Key]
    public Guid PropertyIDNumber { get; set; }

    [Required]
    [MaxLength(150)]
    public string Address { get; set; } = string.Empty;

    [Required]
    public int YearOfConstruction { get; set; }
    [Required]
    public Guid OwnerID { get; set; }
    public User Owner { get; set; }

    public List<Repair> Repairs { get; set; }

    public Property() { }
    public Property(Guid propertyIDNumber, string address, int yearOfConstruction, string ownerVATNumber, Guid ownerID , List<Repair?> repairs)
    {
        PropertyIDNumber = propertyIDNumber;
        Address = address;
        YearOfConstruction = yearOfConstruction;
    }
}
