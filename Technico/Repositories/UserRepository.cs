namespace Technico.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        User? user = await GetAsync(id);
        if (user == null)
            return false;
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<User>> GetAllAsync()
    {
        var owners = new List<User>();
        owners = await _dbContext.Users.ToListAsync();
        return owners;
    }

    public async Task<User?> GetAsync(Guid id)
    {
        return await _dbContext.Users
            .Include(u => u.Properties) 
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> UpdateAsync(User oldUser)
    {
        User? user = await GetAsync(oldUser.Id);
        oldUser.Email = user.Email;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

}
