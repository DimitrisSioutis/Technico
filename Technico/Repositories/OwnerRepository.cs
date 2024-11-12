namespace Technico.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Technico.Context;
using Technico.Models;

public class OwnerRepository : IRepository<Owner, Guid>
{
    private readonly TechnicoDBContext _dbContext;

    public OwnerRepository(TechnicoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Owner?> CreateAsync(Owner owner)
    {
        _dbContext.Owners.Add(owner);
        await _dbContext.SaveChangesAsync();
        return owner;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Owner? owner = await GetAsync(id);
        if (owner == null)
            return false;
        _dbContext.Remove(owner);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Owner?>> GetAllAsync()
    {
        var owners = new List<Owner?>();
        owners = await _dbContext.Owners.ToListAsync();
        return owners;
    }

    public async Task<Owner?> GetAsync(Guid id)
    {
        Owner? owner = await _dbContext.Owners.FindAsync(id);
        return owner;
    }

    public async Task<Owner?> UpdateAsync(Owner oldOwner)
    {
        Owner? owner = await GetAsync(oldOwner.Id);
        oldOwner.Email = owner.Email;
        _dbContext.Owners.Update(owner);
        await _dbContext.SaveChangesAsync();
        return owner;
    }
}
