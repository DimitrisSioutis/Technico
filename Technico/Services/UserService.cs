using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;
using Technico.Interfaces;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Technico.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserSimpleDTO?>> CreateAsync(UserFullDTO createDto)
    {
        var user = new User
        {
            VATNumber = createDto.VATNumber,
            Name = createDto.Name,
            Surname = createDto.Surname,
            Address = createDto.Address,
            PhoneNumber = createDto.PhoneNumber,
            Email = createDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(createDto.Password)
        };

        bool VATexists = await _userRepository.VatExists(user.VATNumber);
        bool emailExists = await _userRepository.EmailExists(user.Email);

        if (VATexists && emailExists)
            return new Result<UserSimpleDTO?>
            {
                Success = false,
                Message = "User with this VAT & Email already exists",
                Data = null
            };

        if (emailExists)
            return new Result<UserSimpleDTO?>
            {
                Success = false,
                Message = "User with this Email already exists",
                Data = null
            };

        if (VATexists)
            return new Result<UserSimpleDTO?>
            {
                Success = false,
                Message = "User with this VAT already exists",
                Data= null
            };

        var result = await _userRepository.CreateAsync(user);
        return new Result<UserSimpleDTO?>
        {
            Success = result != null,
            Data = result == null ? null : new UserSimpleDTO
            {
                Id = result.Id,
                Name = result.Name,
                Surname = result.Surname,
                Email = result.Email
            }
        };
    }

    public async Task<Result<UserSimpleDTO>> DeleteAsync(Guid id)
    {

        var userHasOngoingRepairs = await _userRepository.HasOngoingRepairs(id);
        if (userHasOngoingRepairs)
        {
            return new Result<UserSimpleDTO>
            {
                Success = false,
                Message = "Cannot delete user. There are ongoing repairs associated with this user."
            };
        }

        var deleteResult = await _userRepository.DeleteAsync(id);

        if (!deleteResult)
        {
            return new Result<UserSimpleDTO>
            {
                Success = false,
                Message = "Failed to delete the user. Please try again later."
            };
        }

        return new Result<UserSimpleDTO>
        {
            Success = true,
            Message = "User successfully deleted."
        };
    }

    public async Task<UserFullDTO?> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null) return null;

        var userDTO = new UserFullDTO
        {
            Id = user.Id,
            VATNumber = user.VATNumber,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Address = user.Address,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            Properties = user.Properties.Select(property => new PropertyDTO
            {
                PropertyId = property.PropertyId,
                Address = property.Address,
                YearOfConstruction = property.YearOfConstruction,
                OwnerID = property.OwnerID,
                Repairs = property.Repairs.Select(repair => new RepairDTO
                {
                    Id = repair.Id,
                    ScheduledDate = repair.ScheduledDate,
                    Type = repair.Type,
                    CurrentStatus = repair.CurrentStatus,
                    Cost = repair.Cost,
                    Description = repair.Description,
                    Address = repair.Address,
                    PropertyId = repair.PropertyId

                }).ToList()
            }).ToList()
        };
        return userDTO;
    }


    public async Task<List<UserSimpleDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => new UserSimpleDTO
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
        }).ToList();
    }


    public async Task<Result<UserSimpleDTO?>> UpdateAsync(Guid id, UserFullDTO user)
    {
        var existingUser = await _userRepository.GetAsync(id);
        if (existingUser == null) return null;

        bool emailChanged = existingUser.Email != user.Email;
        bool vatChanged = existingUser.VATNumber != user.VATNumber;

        bool emailExists = false;
        bool vatExists = false;

        if (emailChanged)
            emailExists = await _userRepository.EmailExists(user.Email);

        if (vatChanged)
            vatExists = await _userRepository.VatExists(user.VATNumber);

        if (vatExists && emailExists)
            return new Result<UserSimpleDTO?>
            {
                Success = false,
                Message = "User with this VAT & Email already exists",
                Data = null
            };

        if (emailExists)
            return new Result<UserSimpleDTO?>
            {
                Success = false,
                Message = "User with this Email already exists",
                Data = null
            };

        if (vatExists)
            return new Result<UserSimpleDTO?>
            {
                Success = false,
                Message = "User with this VAT already exists",
                Data = null
            };

        existingUser.VATNumber = user.VATNumber;
        existingUser.Surname = user.Surname;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Address = user.Address;

        var result = await _userRepository.UpdateAsync(existingUser);
        return new Result<UserSimpleDTO?>
        {
            Success = result != null,
            Data = result == null ? null : new UserSimpleDTO
            {
                Id = result.Id,
                Name = result.Name,
                Surname = result.Surname,
                Email = result.Email
            }
        };
    }

    public async Task<List<UserFullDTO>> GetOwnersAsync()
    {
        var users = await _userRepository.GetOwnersAsync();
        return users.Select(user => new UserFullDTO
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            VATNumber = user.VATNumber,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Properties = user.Properties.Select(property => new PropertyDTO
            {
                PropertyId = property.PropertyId,
                Address = property.Address,
                YearOfConstruction = property.YearOfConstruction,
                OwnerID = property.OwnerID
            }).ToList()
        }).ToList();
    }


    public async Task<Result<UserFullDTO?>> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))  
        {
            return new Result<UserFullDTO?>
            {
                Success = false,
                Message = "Invalid email or password",
                Data = null
            };
        }

        var token = GenerateJwtToken(user);

        return new Result<UserFullDTO?>
        {
            Success = true,
            Token = token
        };

    }

    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("NAWHFWIZ7JjQeBvAMe27GWvatNqkKwRG"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, user.Email.ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

        var token = new JwtSecurityToken(
            issuer: "sioutis",
            audience: "technico",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

