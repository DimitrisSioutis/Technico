using System.ComponentModel.DataAnnotations;

namespace Technico.Models;

public class Property
{
    [Key]
    public Guid PropertyIDNumber { get; set; }

    [Required]
    [MaxLength(150)]
    public string Address { get; set; } = string.Empty;

    [Required]
    public int YearOfConstruction { get; set; }

    public int OwnerID { get; set; }

    public List<Repair> Repairs { get; set; }

    public Property() { }
    public Property(Guid propertyIDNumber, string address, int yearOfConstruction, string ownerVATNumber, int ownerID , List<Repair?> repairs)
    {
        PropertyIDNumber = propertyIDNumber;
        Address = address;
        YearOfConstruction = yearOfConstruction;
        OwnerID = ownerID;
        Repairs = repairs;
    }
}
