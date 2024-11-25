using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Technico.Dtos;

namespace Technico.Interfaces;

public interface IPropertyService
{
    Task<PropertyDTO?> CreateAsync(PropertyDTO propertyDTO);
    Task<bool> DeleteAsync(Guid id);
    Task<List<SimplePropertyDTO?>> GetAllAsync();
    Task<PropertyDTO?> GetAsync(Guid id);
    Task<PropertyDTO?> UpdateAsync(Guid id, PropertyDTO propertyDTO);
}
