using Technico.Models;
using Technico.Repositories;

namespace Technico.Services;

public class OwnerService : IService<Owner, Guid>
{
    private readonly IRepository<Owner, Guid> _ownerRepository;

    public OwnerService(IRepository<Owner, Guid> ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<Owner?> CreateAsync(Owner owner)
    {
        await _ownerRepository.CreateAsync(owner);
        return owner;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _ownerRepository.DeleteAsync(id);
        return true;
    }

    public async Task<List<Owner?>> GetAllAsync()
    {
        List<Owner?> owners = await _ownerRepository.GetAllAsync();
        return  owners;
    }

    public async Task<Owner?> GetAsync(Guid id)
    {
        Owner? owner = await _ownerRepository.GetAsync(id); 
        return owner;
    }

    public async Task<Owner?> UpdateAsync(Owner owner)
    {
        await _ownerRepository.CreateAsync(owner);
        return owner;
    }

}
