using System.Collections.Generic;
using System.Threading.Tasks;
using Technico.Models;
using Technico.Repositories;

namespace Technico.Services
{
    public class PropertyService : IService<Property, Guid>
    {
        private readonly IRepository<Property, Guid> _propertyRepository;

        public PropertyService(IRepository<Property, Guid> propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Property?> CreateAsync(Property property)
        {
            await _propertyRepository.CreateAsync(property);
            return property;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _propertyRepository.DeleteAsync(id);
        }

        public async Task<List<Property?>> GetAllAsync()
        {
            return await _propertyRepository.GetAllAsync();
        }

        public async Task<Property?> GetAsync(Guid id)
        {
            return await _propertyRepository.GetAsync(id);
        }

        public async Task<Property?> UpdateAsync(Property property)
        {
            return await _propertyRepository.UpdateAsync(property);
        }
    }
}
