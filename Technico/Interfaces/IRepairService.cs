using Technico.Dtos;
using Technico.Models;

namespace Technico.Interfaces;

public interface IRepairService
{
    Task<Repair?> CreateAsync(RepairDTO repairDto);
    Task<bool> DeleteAsync(Guid repairId);
    Task<List<RepairDTO>> GetAllAsync();
    Task<RepairDTO?> GetAsync(Guid repairId);
    Task<RepairDTO?> UpdateAsync(Guid id, RepairDTO repair);
}
