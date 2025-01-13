using Microsoft.EntityFrameworkCore;
using Technico.Models;

namespace Technico.Repositories;

public interface IRepairRepository
{
    Task<Repair> CreateAsync(Repair repair);
    Task<bool> DeleteAsync(Guid repairId);
    Task<List<Repair>> GetAllAsync();
    Task<Repair?> GetAsync(Guid repairId);
    Task<Repair?> UpdateAsync(Repair updatedRepair);
    Task<List<Repair>> GetDailyAsync();
    Task<List<Repair>> GetOngoingAsync();
}

public class RepairRepository : IRepairRepository
{
    private readonly TechnicoDBContext _dbContext;

    public RepairRepository(TechnicoDBContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Repair> CreateAsync(Repair repair)
    {
        ArgumentNullException.ThrowIfNull(repair);

        _dbContext.Repairs.Add(repair);
        await _dbContext.SaveChangesAsync();

        // Reload the repair with related property data
        return await GetAsync(repair.Id) ?? repair;
    }

    public async Task<bool> DeleteAsync(Guid repairId)
    {
        var repair = await _dbContext.Repairs.FindAsync(repairId);
        if (repair is null)
        {
            return false;
        }

        _dbContext.Repairs.Remove(repair);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Repair>> GetAllAsync()
    {
        return await _dbContext.Repairs
            .Include(r => r.RepairingProperty)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Repair?> GetAsync(Guid repairId)
    {
        return await _dbContext.Repairs
            .Include(r => r.RepairingProperty)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == repairId);
    }

    public async Task<Repair?> UpdateAsync(Repair updatedRepair)
    {
        ArgumentNullException.ThrowIfNull(updatedRepair);

        var repair = await _dbContext.Repairs
            .Include(r => r.RepairingProperty)
            .FirstOrDefaultAsync(r => r.Id == updatedRepair.Id);

        if (repair is null)
        {
            return null;
        }

        _dbContext.Entry(repair).CurrentValues.SetValues(updatedRepair);

        if (updatedRepair.RepairingProperty is not null)
        {
            repair.RepairingProperty = updatedRepair.RepairingProperty;
        }

        await _dbContext.SaveChangesAsync();
        return repair;
    }

    public async Task<List<Repair>> GetDailyAsync()
    {
        var today = DateTime.Today;
        return await _dbContext.Repairs
            .Include(r => r.RepairingProperty)
            .AsNoTracking()
            .Where(r => r.ScheduledDate.Date == today)
            .ToListAsync();
    }

    public async Task<List<Repair>> GetOngoingAsync()
    {
        return await _dbContext.Repairs
            .Include(r => r.RepairingProperty)
            .AsNoTracking()
            .Where(r => r.CurrentStatus != Repair.Status.Completed)
            .ToListAsync();
    }
}