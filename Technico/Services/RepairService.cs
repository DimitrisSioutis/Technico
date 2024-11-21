using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;

namespace Technico.Services;

public class RepairService
{
    private readonly RepairRepository _repairRepository;

    public RepairService(RepairRepository repairRepository)
    {
        _repairRepository = repairRepository;
    }

    public async Task<Repair?> CreateAsync(RepairDTO repairDto)
    {
            var repair = new Repair
            {
                Id = Guid.NewGuid(),
                ScheduledDate = repairDto.ScheduledDate,
                Type = repairDto.Type,
                CurrentStatus = repairDto.CurrentStatus,
                Description = repairDto.Description,
                Address = repairDto.Address,
                Cost = repairDto.Cost,
                PropertyId = repairDto.PropertyId
            };
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
