using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;
using Technico.Interfaces;

namespace Technico.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserSimpleDTO?> CreateAsync(UserFullDTO createDto)
    {
        var users = await _userRepository.GetAllAsync();

        bool VATexists = users.Any(u => u?.VATNumber == createDto.VATNumber);
        if (VATexists)return null;

        bool emailExists = users.Any(u => u.Email == createDto.Email);
        if (emailExists) return null;

        var user = new User
        {
            VATNumber = createDto.VATNumber,
            Name = createDto.Name,
            Surname = createDto.Surname,
            Address = createDto.Address,
            PhoneNumber = createDto.PhoneNumber,
            Email = createDto.Email,
            Password = createDto.Password,
        };
        var result = await _userRepository.CreateAsync(user);

        return result == null ? null : new UserSimpleDTO
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email
        };
    }


    public async Task<UserSimpleDTO?> UpdateAsync(Guid id, UserFullDTO user)
    {
        var existingUser = await _userRepository.GetAsync(id);
        if (existingUser == null) return null;

        // Update the fields
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;

        var result = await _userRepository.UpdateAsync(existingUser);

        return new UserSimpleDTO
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<UserFullDTO?> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null) return null;


        foreach (var property in user.Properties)
        {
            Console.WriteLine($"Property: ID={property.PropertyId}, Address={property.Address}, OwnerID={property.OwnerID}");
        }

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
            Properties = user.Properties.Select(property => new SimplePropertyDTO
            {
                PropertyId = property.PropertyId,
                Address = property.Address,
                YearOfConstruction = property.YearOfConstruction
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
}
