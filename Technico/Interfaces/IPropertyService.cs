using Technico.Dtos;
using Technico.Models;

namespace Technico.Interfaces;

public interface IPropertyService
{
    Task<Result<PropertyDTO>> CreateAsync(PropertyCreateDTO propertyDTO);
    Task<bool> DeleteAsync(Guid id);
    Task<List<SimplePropertyDTO?>> GetAllAsync();
    Task<PropertyDTO?> GetAsync(Guid id);
    Task<PropertyDTO?> UpdateAsync(Guid id, PropertyDTO propertyDTO);
}
