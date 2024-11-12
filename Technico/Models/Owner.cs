namespace Technico.Models;

public class Owner : User
{
    public List<Property> Properties { get; set; } = new List<Property>();
    public Owner() { }
    public Owner(Guid id,string vatNumber, string name, string surname, string address, string phoneNumber, string email, string password, Type userType, List<Property> properties)
        : base(id, vatNumber, name, surname, address, phoneNumber, email, password, userType)
    {
        Properties = properties;
    }
}
