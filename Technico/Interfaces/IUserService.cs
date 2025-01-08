using Technico.Dtos;
using Technico.Models;

namespace Technico.Interfaces;

public interface IUserService
{
    Task<Result<UserSimpleDTO?>> CreateAsync(UserFullDTO createDto);
    Task<Result<UserSimpleDTO?>> UpdateAsync(Guid id, UserFullDTO user);
    Task<Result<UserSimpleDTO>> DeleteAsync(Guid id);
    Task<UserFullDTO?> GetAsync(Guid id);
    Task<List<UserSimpleDTO>> GetAllAsync();
    Task<List<UserFullDTO>> GetOwnersAsync();
    Task<Result<UserFullDTO?>> LoginAsync(string email,string password);
}
