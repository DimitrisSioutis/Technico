using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;
using Technico.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Technico.Mappers;

namespace Technico.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public UserService(UserRepository userRepository, IConfiguration configuration ,IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _configuration = configuration;
    }
    public async Task<UserSimpleDTO> CreateAsync(UserCreateDTO createDto)
    {
        var user = _mapper.MapToUser(createDto);

        var existingUser = await _userRepository.GetByEmailOrVatAsync(user.Email, user.VATNumber);
        if (existingUser)
        {
            throw new InvalidOperationException("User with this VAT or Email already exists");
        }

        var result = await _userRepository.CreateAsync(user);
        if (result == null)
        {
            throw new Exception("Failed to create user");
        }

        return _mapper.MapToUserSimpleDTO(result);
    }


    public async Task<string> DeleteAsync(Guid id)
    {
        var userHasOngoingRepairs = await _userRepository.HasOngoingRepairs(id);
        if (userHasOngoingRepairs)
        {
            throw new InvalidOperationException("User is associated with ongoing repairs");
        }

        var deleteResult = await _userRepository.DeleteAsync(id);

        if (!deleteResult)
        {
            throw new Exception("Failed to create user");
        }

        return "User deleted succesfully";
    }

    public async Task<UserFullDTO?> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null) return null;

        return _mapper.MapToUserFullDTO(user);
    }

    public async Task<List<UserSimpleDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => _mapper.MapToUserSimpleDTO(user)).ToList();
    }

    public async Task<UserSimpleDTO?> UpdateAsync(Guid id, UserFullDTO user)
    {
        var existingUser = await _userRepository.GetAsync(id);
        if (existingUser == null) throw new KeyNotFoundException("User not found");

        bool emailChanged = existingUser.Email != user.Email;
        bool vatChanged = existingUser.VATNumber != user.VATNumber;

        if ((emailChanged || vatChanged) && await _userRepository.GetByEmailOrVatAsync(user.Email, user.VATNumber))
            throw new InvalidOperationException("User with this Email or VAT already exists");

        existingUser.VATNumber = user.VATNumber;
        existingUser.Surname = user.Surname;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Address = user.Address;

        var updatedUser = await _userRepository.UpdateAsync(existingUser);
        if (updatedUser == null) throw new Exception("Failed to update user");

        return _mapper.MapToUserSimpleDTO(updatedUser);
    }


    public async Task<List<UserFullDTO>> GetOwnersAsync()
    {
        var users = await _userRepository.GetOwnersAsync();
        return users.Select(user => _mapper.MapToUserFullDTO(user)).ToList();
    }


    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;  
        }

        var token = GenerateJwtToken(user);
        return token;  
    }


    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, user.Email.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
