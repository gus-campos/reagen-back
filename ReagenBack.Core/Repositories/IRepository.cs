using ReagenBack.Core.Models;

namespace ReagenBack.Core.Repositories;

public interface IRepository<TEntity>
    where TEntity : IWithId
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}