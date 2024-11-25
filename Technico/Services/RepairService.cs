using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;
using Technico.Interfaces;

namespace Technico.Services;

public class RepairService : IRepairService
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

    public async Task<List<RepairDTO>> GetAllAsync()
    {
        var repairs = await _repairRepository.GetAllAsync();
        return repairs.Select(repair => new RepairDTO
        {
            Id = repair.Id,
            ScheduledDate = repair.ScheduledDate,
            Type = repair.Type,
            CurrentStatus = repair.CurrentStatus,
            Description = repair.Description,
            Address = repair.Address,
            Cost = repair.Cost,
            PropertyId = repair.PropertyId
        }).ToList();
    }

    public async Task<RepairDTO?> GetAsync(Guid repairId)
    {
        var repair = await _repairRepository.GetAsync(repairId);
        if (repair == null)
            return null;

        return new RepairDTO
        {
            Id = repair.Id,
            ScheduledDate = repair.ScheduledDate,
            Type = repair.Type,
            CurrentStatus = repair.CurrentStatus,
            Description = repair.Description,
            Address = repair.Address,
            Cost = repair.Cost,
            PropertyId = repair.PropertyId
        };
    }
    public async Task<RepairDTO?> UpdateAsync(Guid id,RepairDTO repair)
    {
        if (id != repair.Id)
            return null;

        var existingRepair = await _repairRepository.GetAsync(id);
        if (existingRepair == null)
            return null;

        existingRepair.ScheduledDate = repair.ScheduledDate;
        existingRepair.Type = repair.Type;
        existingRepair.CurrentStatus = repair.CurrentStatus;
        existingRepair.Description = repair.Description;
        existingRepair.Address = repair.Address;
        existingRepair.Cost = repair.Cost;

        var result = await _repairRepository.UpdateAsync(existingRepair);
        if (result == null)
            return null;

        return new RepairDTO
        {
            PropertyId = result.PropertyId
        };
    }
}
