namespace Technico.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Technico.Context;
using Technico.Models;

public class UserRepository
{
    private readonly TechnicoDBContext _dbContext;

    public UserRepository(TechnicoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> CreateAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
        {
            return false; 
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;  
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<List<User>> GetOwnersAsync()
    {
        return await _dbContext.Users.Include(u => u.Properties).Where(u => u.Role == 0).ToListAsync();
    }

    public async Task<User?> GetAsync(Guid id)
    {
        return await _dbContext.Users
            .Include(u => u.Properties) 
                .ThenInclude(p => p.Repairs)      
            .FirstOrDefaultAsync(u => u.Id == id);
    }


    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> UpdateAsync(User oldUser)
    {
        User? user = await GetAsync(oldUser.Id);
        oldUser.Email = user.Email;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> EmailExists(String email)
    {
        bool emailExists = await _dbContext.Users.AnyAsync(u => u.Email == email);
        return emailExists;
    }

    public async Task<bool> VatExists(String vatNumber)
    {
        bool emailExists = await _dbContext.Users.AnyAsync(u => u.VATNumber == vatNumber);
        return emailExists;
    }

    public async Task<bool> HasOngoingRepairs(Guid id)
    {
        var user = await _dbContext.Users.Include(u => u.Properties).ThenInclude(p => p.Repairs).FirstOrDefaultAsync(u => u.Id == id);
        return user.Properties
            .Any(p =>p.Repairs
            .Any(repair =>repair.CurrentStatus == Repair.Status.Pending || repair.CurrentStatus == Repair.Status.InProgress));
    }
}
