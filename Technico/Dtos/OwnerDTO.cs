using Technico.Models;

namespace Technico.Dtos;

public class OwnerDTO : UserDTO
{
    public List<PropertyDTO> Properties { get; set; } = new List<PropertyDTO>();

    public OwnerDTO() { }

    public OwnerDTO(Guid id, string vatNumber, string name, string surname, string address, string phoneNumber, string email, string password, Owner.Type userType, List<PropertyDTO> properties)
        : base(id, vatNumber, name, surname, address, phoneNumber, email, password, userType)
    {
        Properties = properties;
    }
}