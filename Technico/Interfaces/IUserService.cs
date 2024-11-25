using Technico.Dtos;

namespace Technico.Interfaces;

public interface IUserService
{
    Task<UserSimpleDTO?> CreateAsync(UserFullDTO createDto);
    Task<UserSimpleDTO?> UpdateAsync(Guid id, UserFullDTO user);
    Task<bool> DeleteAsync(Guid id);
    Task<UserFullDTO?> GetAsync(Guid id);
    Task<List<UserSimpleDTO>> GetAllAsync();
}
