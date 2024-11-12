using Technico.Models;
using Technico.Repositories;

namespace Technico.Services;

public class ProfessionalService: IService<Professional, Guid>
{
    private readonly IRepository<Professional, Guid> _professionalRepository;

    public ProfessionalService(IRepository<Professional, Guid> professionalRepository)
    {
        _professionalRepository = professionalRepository;
    }

    public async Task<Professional?> CreateAsync(Professional pro)
    {
        return await _professionalRepository.CreateAsync(pro);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _professionalRepository.DeleteAsync(id);  
    }

    public async Task<List<Professional?>> GetAllAsync()
    {
        List<Professional?> pros = await _professionalRepository.GetAllAsync();
        return pros;
    }

    public async Task<Professional?> GetAsync(Guid id)
    {
        Professional? pro = await _professionalRepository.GetAsync(id);
        return pro;
    }

    public async Task<Professional?> UpdateAsync(Professional pro)
    {
        return await _professionalRepository.UpdateAsync(pro);
    }


}
