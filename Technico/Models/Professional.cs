namespace Technico.Models;
public class Professional : User
{
    public Professional() { }

    public Professional(Guid id,string vatNumber, string name, string surname, string address, string phoneNumber, string email, string password, Type userType)
        : base(id,vatNumber, name, surname, address, phoneNumber, email, password, userType)
    {

    }
}
