namespace Technico.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Technico.Context;
using Technico.Models;

public class ProfessionalRepository : IRepository<Professional, Guid>
{
    private readonly TechnicoDBContext _dbContext;

    public ProfessionalRepository(TechnicoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Professional?> CreateAsync(Professional professional)
    {
        _dbContext.Professionals.Add(professional);
        await _dbContext.SaveChangesAsync();
        return professional;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Professional? professional = await GetAsync(id);
        if (professional == null)
            return false;
        _dbContext.Professionals.Remove(professional);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Professional?>> GetAllAsync()
    {
        var professionals = new List<Professional?>();
        professionals = await _dbContext.Professionals.ToListAsync();
        return professionals;
    }

    public async Task<Professional?> GetAsync(Guid id)
    {
        Professional? professional = await _dbContext.Professionals.FindAsync(id);
        return professional;
    }

    public async Task<Professional?> UpdateAsync(Professional oldPro)
    {
        Professional? pro = await GetAsync(oldPro.Id);
        oldPro.Email = pro.Email;
        _dbContext.Professionals.Update(pro);
        await _dbContext.SaveChangesAsync();
        return pro;
    }
}
