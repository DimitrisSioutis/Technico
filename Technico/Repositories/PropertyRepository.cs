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
            Console.WriteLine($"Creating Property - Address: {property.Address}, OwnerID: {property.OwnerID}");

            _dbContext.Properties.Add(property);
            await _dbContext.SaveChangesAsync();

            Console.WriteLine($"Created Property - Address: {property.Address}, OwnerID: {property.OwnerID}");

            return property;
        }

        public async Task<bool> DeleteAsync(Guid propertyIdNumber)
        {
            Property? property = await GetAsync(propertyIdNumber);
            if (property == null)
                return false;
            _dbContext.Properties.Remove(property);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Property?>> GetAllAsync()
        {
            return await _dbContext.Properties.ToListAsync();
        }

        public async Task<Property?> GetAsync(Guid propertyIdNumber)
        {
            return await _dbContext.Properties.FindAsync(propertyIdNumber);
        }

        public async Task<Property?> UpdateAsync(Property updatedProperty)
        {
            var property = await GetAsync(updatedProperty.PropertyIDNumber);
            if (property == null)
                return null;

            property.Address = updatedProperty.Address;
            property.YearOfConstruction = updatedProperty.YearOfConstruction;

            _dbContext.Properties.Update(property);
            await _dbContext.SaveChangesAsync();
            return property;
        }
    }
}
