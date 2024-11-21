using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Technico.Models;

[Index(nameof(Address), IsUnique = true)]
public class Property
{
    [Key]
    public Guid PropertyId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Address { get; set; } = string.Empty;

    [Required]
    public int YearOfConstruction { get; set; }
    [Required]
    public Guid OwnerID { get; set; }
    public User Owner { get; set; }
    public virtual List<Repair> Repairs { get; set; } = new List<Repair>();

    public Property() { }
    public Property(Guid propertyId, string address, int yearOfConstruction, string ownerVATNumber, Guid ownerID , List<Repair?> repairs)
    {
        PropertyId = propertyId;
        Address = address;
        YearOfConstruction = yearOfConstruction;
    }
}
