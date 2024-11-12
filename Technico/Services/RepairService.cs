using Technico.Models;
using Technico.Repositories;

namespace Technico.Services
{
    public class RepairService : IService<Repair, Guid>
    {
        private readonly IRepository<Repair, Guid> _repairRepository;

        public RepairService(IRepository<Repair, Guid> repairRepository)
        {
            _repairRepository = repairRepository;
        }

        public async Task<Repair?> CreateAsync(Repair repair)
        {
            await _repairRepository.CreateAsync(repair);
            return repair;
        }

        public async Task<bool> DeleteAsync(Guid repairId)
        {
            return await _repairRepository.DeleteAsync(repairId);
        }

        public async Task<List<Repair?>> GetAllAsync()
        {
            return await _repairRepository.GetAllAsync();
        }

        public async Task<Repair?> GetAsync(Guid repairId)
        {
            return await _repairRepository.GetAsync(repairId);
        }

        public async Task<Repair?> UpdateAsync(Repair repair)
        {
            return await _repairRepository.UpdateAsync(repair);
        }
    }
}
