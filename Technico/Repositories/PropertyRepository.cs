using Microsoft.EntityFrameworkCore;
using Technico.Models;

namespace Technico.Repositories;

public interface IPropertyRepository
{
    Task<Property?> CreateAsync(Property property);
    Task<bool> DeleteAsync(Guid propertyId);
    Task<List<Property>> GetAllAsync();
    Task<Property?> GetAsync(Guid propertyId);
    Task<Property?> UpdateAsync(Property property);
}

public class PropertyRepository : IPropertyRepository
{
    private readonly TechnicoDBContext _dbContext;

    public PropertyRepository(TechnicoDBContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Property?> CreateAsync(Property property)
    {
        ArgumentNullException.ThrowIfNull(property);

        _dbContext.Properties.Add(property);
        await _dbContext.SaveChangesAsync();

        // Return the property with related data loaded
        return await GetAsync(property.PropertyId);
    }

    public async Task<bool> DeleteAsync(Guid propertyId)
    {
        var property = await _dbContext.Properties.FindAsync(propertyId);
        if (property is null)
        {
            return false;
        }

        _dbContext.Properties.Remove(property);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _dbContext.Properties
            .Include(p => p.Repairs)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Property?> GetAsync(Guid propertyId)
    {
        return await _dbContext.Properties
            .Include(p => p.Repairs)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PropertyId == propertyId);
    }

    public async Task<Property?> UpdateAsync(Property updatedProperty)
    {
        ArgumentNullException.ThrowIfNull(updatedProperty);

        var property = await _dbContext.Properties
            .Include(p => p.Repairs)
            .FirstOrDefaultAsync(p => p.PropertyId == updatedProperty.PropertyId);

        if (property is null)
        {
            return null;
        }

        _dbContext.Entry(property).CurrentValues.SetValues(updatedProperty);

        await _dbContext.SaveChangesAsync();
        return property;
    }
}