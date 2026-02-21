using ReagenBack.Core.Models;

namespace ReagenBack.Core.Services;

public interface ICrudService<TEntity, TCreateDto, TReadDto> 
    where TEntity : class, IWithId
{
    Task<IEnumerable<TReadDto>> GetAllAsync();
    Task<TReadDto?> GetByIdAsync(int id);
    Task<TReadDto> AddAsync(TCreateDto entity);
    Task<TReadDto?> UpdateAsync(int id, TCreateDto entity);
    Task<bool> DeleteAsync(int id);
}