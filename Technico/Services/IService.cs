using Microsoft.Extensions.Hosting;

namespace Technico.Services;

public interface IService<T, K> where T : class
{
    Task<T?> CreateAsync(T t);
    Task<T?> UpdateAsync(T t);
    Task<bool> DeleteAsync(K id);
    Task<T?> GetAsync(K id);
    Task<List<T?>> GetAllAsync();
}