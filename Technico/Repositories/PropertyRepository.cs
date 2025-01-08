using Microsoft.EntityFrameworkCore;
using Technico.Context;
using Technico.Models;

namespace Technico.Repositories
{
    public class PropertyRepository
    {
        private readonly TechnicoDBContext _dbContext;

        public PropertyRepository(TechnicoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Property?> CreateAsync(Property property)
        { 
            _dbContext.Properties.Add(property);
            await _dbContext.SaveChangesAsync();

            return property;
        }

        public async Task<bool> DeleteAsync(Guid propertyId)
        {
            Property? property = await GetAsync(propertyId);
            if (property == null)
                return false;
            _dbContext.Properties.Remove(property);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _dbContext.Properties.ToListAsync();
        }

        public async Task<Property?> GetAsync(Guid propertyId)
        {
            return await _dbContext.Properties
                                    .Include(p => p.Repairs)
                                    .FirstOrDefaultAsync(p => p.PropertyId == propertyId);
        }


        public async Task<Property?> UpdateAsync(Property oldProperty)
        {
            var property = await GetAsync(oldProperty.PropertyId);
            if (property == null)
                return null;

            property.Address = oldProperty.Address;
            property.YearOfConstruction = oldProperty.YearOfConstruction;

            _dbContext.Properties.Update(property);
            await _dbContext.SaveChangesAsync();
            return property;
        }
    }
}
