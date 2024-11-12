namespace Technico.Dtos;

public class PropertyDTO
{
    public Guid PropertyIDNumber { get; set; }
    public string Address { get; set; } = string.Empty;
    public int YearOfConstruction { get; set; }
    public int OwnerID { get; set; }
    public List<RepairDTO> Repairs { get; set; }

    public PropertyDTO() { }

    public PropertyDTO(Guid propertyIDNumber, string address, int yearOfConstruction, int ownerID, List<RepairDTO> repairs)
    {
        PropertyIDNumber = propertyIDNumber;
        Address = address;
        YearOfConstruction = yearOfConstruction;
        OwnerID = ownerID;
        Repairs = repairs;
    }
}