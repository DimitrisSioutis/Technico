using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;

namespace Technico.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDTO?> CreateAsync(UserRequestDTO createDto)
    {
        var users = await _userRepository.GetAllAsync();

        bool VATexists = users.Any(u => u.VATNumber == createDto.VATNumber);
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

        return result == null ? null : new UserResponseDTO
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email
        };
    }


    public async Task<UserResponseDTO?> UpdateAsync(Guid id, UserRequestDTO user)
    {
        var existingUser = await _userRepository.GetAsync(id);
        if (existingUser == null) return null;

        // Update the fields
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;

        var result = await _userRepository.UpdateAsync(existingUser);

        return new UserResponseDTO
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

    public async Task<UserResponseDTO?> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null) return null;


        var userDTO = new UserResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Properties = user.Properties.Select(property => new PropertyDTO
            {
                PropertyIDNumber = property.PropertyIDNumber,
                Address = property.Address,
                YearOfConstruction = property.YearOfConstruction
            }).ToList()
        };

        return userDTO;
    }

    public async Task<List<UserResponseDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => new UserResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        }).ToList();
    }
}
