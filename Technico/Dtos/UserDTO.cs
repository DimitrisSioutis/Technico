namespace Technico.Dtos;

public class UserFullDTO
{
    public Guid Id { get; set; }
    public string VATNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public virtual List<SimplePropertyDTO> Properties { get; set; } = new List<SimplePropertyDTO>();
}

public class UserSimpleDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
