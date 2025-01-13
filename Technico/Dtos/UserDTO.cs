using System.ComponentModel.DataAnnotations;
using Technico.Models;

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

    public User.Type Role { get; set; }

    public string Password { get; set; } = string.Empty;
    public virtual List<PropertyDTO> Properties { get; set; } = new List<PropertyDTO>();
}

public class UserSimpleDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserCreateDTO
{
    [Required(ErrorMessage = "VAT number is required")]
    [StringLength(12, MinimumLength = 9, ErrorMessage = "VAT number must be between 9 to 12 characters")]
    public string VATNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s-']+$", ErrorMessage = "Name can only contain letters, spaces, hyphens and apostrophes")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(50, ErrorMessage = "Surname must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s-']+$", ErrorMessage = "Surname can only contain letters, spaces, hyphens and apostrophes")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address must be less than 200 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
    public string Password { get; set; } = string.Empty;
}
