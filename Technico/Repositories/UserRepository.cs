namespace Technico.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Technico.Models;

public interface IUserRepository
{
    Task<User?> CreateAsync(User user);
    Task<bool> DeleteAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task<List<User>> GetOwnersAsync();
    Task<User?> GetAsync(Guid id);
    Task<User> UpdateAsync(User user);
    Task<bool> GetByEmailOrVatAsync(string email, string vatNumber);
    Task<bool> HasOngoingRepairs(Guid id);

    Task<User?> GetByEmailAsync(string email);
}

public class UserRepository : IUserRepository
{
    private readonly TechnicoDBContext _dbContext;

    public UserRepository(TechnicoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> CreateAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user is null)
        {
            return false;
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<User>> GetOwnersAsync()
    {
        return await _dbContext.Users
            .Include(u => u.Properties)
            .AsNoTracking()
            .Where(u => u.Role == 0)
            .ToListAsync();
    }

    public async Task<User?> GetAsync(Guid id)
    {
        return await _dbContext.Users
            .Include(u => u.Properties)
                .ThenInclude(p => p.Repairs)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> GetByEmailOrVatAsync(string email, string vatNumber)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email || u.VATNumber == vatNumber);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> UpdateAsync(User updatedUser)
    {
        ArgumentNullException.ThrowIfNull(updatedUser);

        var existingUser = await GetAsync(updatedUser.Id)
            ?? throw new KeyNotFoundException($"User with ID {updatedUser.Id} not found");

        existingUser.Name = updatedUser.Name ?? existingUser.Name;
        existingUser.Surname = updatedUser.Surname ?? existingUser.Surname;
        existingUser.Email = updatedUser.Email ?? existingUser.Email;
        existingUser.PhoneNumber = updatedUser.PhoneNumber ?? existingUser.PhoneNumber;
        existingUser.Address = updatedUser.Address ?? existingUser.Address;
        existingUser.VATNumber = updatedUser.VATNumber ?? existingUser.VATNumber;


        if (!string.IsNullOrEmpty(updatedUser.Password))
        {
            existingUser.Password = updatedUser.Password;
        }

        _dbContext.Users.Update(existingUser);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Database update error: " + ex.Message);
            throw new Exception("Failed to update user due to a database error.");
        }
        var updatedEntity = await _dbContext.Users.FindAsync(updatedUser.Id);
        if (updatedEntity == null)
        {
            throw new KeyNotFoundException($"User with ID {updatedUser.Id} not found after update");
        }

        return updatedEntity;
    }


    public async Task<bool> HasOngoingRepairs(Guid id)
    {
        var user = await _dbContext.Users
            .Include(u => u.Properties)
                .ThenInclude(p => p.Repairs)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return user?.Properties
            .Any(p => p.Repairs
                .Any(repair => repair.CurrentStatus is
                    Repair.Status.Pending or
                    Repair.Status.InProgress)) ?? false;
    }
}