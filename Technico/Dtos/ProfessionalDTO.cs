using Technico.Models;

namespace Technico.Dtos;

public class ProfessionalDTO : UserDTO
{
    public ProfessionalDTO() { }

    public ProfessionalDTO(Guid id, string vatNumber, string name, string surname, string address, string phoneNumber, string email, string password, User.Type userType)
        : base(id, vatNumber, name, surname, address, phoneNumber, email, password, userType)
    {
    }
}