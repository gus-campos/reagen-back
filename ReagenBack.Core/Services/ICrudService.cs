using ReagenBack.Core.Models;

namespace ReagenBack.Core.Services;

public interface ICrudService<TEntity, TCreateDto, TReadDto> 
    where TEntity : class, IEntity
{
    Task<IEnumerable<TReadDto>> GetAllAsync();
    Task<TReadDto?> GetByIdAsync(int id);
    Task<TReadDto> AddAsync(TCreateDto entity);
    Task<TReadDto?> UpdateAsync(int id, TCreateDto entity);
    Task<bool> DeleteAsync(int id);
}