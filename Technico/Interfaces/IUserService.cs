using Technico.Dtos;
using Technico.Models;

namespace Technico.Interfaces;

public interface IUserService
{
    Task<UserSimpleDTO?> CreateAsync(UserCreateDTO createDto);
    Task<UserSimpleDTO?> UpdateAsync(Guid id, UserFullDTO user);
    Task<string> DeleteAsync(Guid id);
    Task<UserFullDTO?> GetAsync(Guid id);
    Task<List<UserSimpleDTO>> GetAllAsync();
    Task<List<UserFullDTO>> GetOwnersAsync();
    Task<string> LoginAsync(string email,string password);
}
