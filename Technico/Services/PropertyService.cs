using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;
using Technico.Interfaces;

namespace Technico.Services;

public class PropertyService : IPropertyService
{
    private readonly PropertyRepository _propertyRepository;

    public PropertyService(PropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<PropertyDTO>> CreateAsync(PropertyCreateDTO propertyDTO)
    {
        Console.WriteLine(propertyDTO);
        var properties = await _propertyRepository.GetAllAsync();
        bool propertyExists = properties.Any(u => u?.Address == propertyDTO.Address);

        if (propertyExists)
        {
            return new Result<PropertyDTO>
            {
                Success = false,
                Message = "A property with the same address already exists."
            };
        }

        var property = new Property
        {
            Address = propertyDTO.Address,
            YearOfConstruction = propertyDTO.YearOfConstruction,
            OwnerID = propertyDTO.OwnerID
        };

        var result = await _propertyRepository.CreateAsync(property);

        return new Result<PropertyDTO>
        {
            Success = result != null,
            Data = result == null ? null : new PropertyDTO
            {
                Address = result.Address,
                YearOfConstruction = result.YearOfConstruction,
                OwnerID = result.OwnerID
            }
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _propertyRepository.DeleteAsync(id);
    }

    public async Task<List<SimplePropertyDTO?>> GetAllAsync()
    {
        var properties = await _propertyRepository.GetAllAsync();
        var propertyDTOs = properties.Select(property => new SimplePropertyDTO
        {
            PropertyId = property.PropertyId,
            Address = property.Address,
            YearOfConstruction = property.YearOfConstruction,
            
        }).ToList();
        return propertyDTOs;
    }

    public async Task<PropertyDTO?> GetAsync(Guid id)
    {
        var property = await _propertyRepository.GetAsync(id);
        if (property == null) return null;

        var propertyDTO = new PropertyDTO
        {
            PropertyId = property.PropertyId,
            Address = property.Address,
            YearOfConstruction = property.YearOfConstruction,
            OwnerID = property.OwnerID,
            Repairs = property.Repairs?.Select(repair => new RepairDTO
            {
                Id = repair.Id,
                ScheduledDate = repair.ScheduledDate,
                Type = repair.Type,
                CurrentStatus = repair.CurrentStatus,
                Cost = repair.Cost,
                Description = repair.Description,
                Address = repair.Address,
                PropertyId= repair.PropertyId

            }).ToList() ?? new List<RepairDTO>()
        };

        return propertyDTO;
    }
    public async Task<PropertyDTO?> UpdateAsync(Guid id, PropertyDTO propertyDTO)
    {
        Console.WriteLine(propertyDTO);
        if (id != propertyDTO.PropertyId)
            return null;

        var existingProperty = await _propertyRepository.GetAsync(id);
        if (existingProperty == null)
            return null;

        existingProperty.Address = propertyDTO.Address;
        existingProperty.YearOfConstruction = propertyDTO.YearOfConstruction;

        var result = await _propertyRepository.UpdateAsync(existingProperty);
        if (result == null)
            return null;

        Console.WriteLine(result.Address,result.OwnerID,result.YearOfConstruction);
        return new PropertyDTO
        {
            PropertyId = result.PropertyId,
            Address = result.Address,
            OwnerID = result.OwnerID,
            YearOfConstruction = result.YearOfConstruction
        };
    }
}
