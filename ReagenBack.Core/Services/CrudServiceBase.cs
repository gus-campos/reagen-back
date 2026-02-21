
using ReagenBack.Core.Models;
using ReagenBack.Core.Repositories;

namespace ReagenBack.Core.Services;

public abstract class CrudServiceBase<TEntity, TCreateDto, TReadDto>(
    IRepository<TEntity> repository,
    IMapper<TEntity, TCreateDto, TReadDto> mapper
) : ICrudService<TEntity, TCreateDto, TReadDto>
    where TEntity : class, IWithId
    where TCreateDto : class
    where TReadDto : class, IWithId
{
    protected readonly IRepository<TEntity> _repository = repository;

    public async Task<IEnumerable<TReadDto>> GetAllAsync() {

        var entities = await _repository.GetAllAsync();
        return entities.Select(mapper.ToReadDto);
    }

    public async Task<TReadDto?> GetByIdAsync(int id) {

        var entity = await _repository.GetByIdAsync(id);
        
        if (entity == null) return null;
        
        return mapper.ToReadDto(entity);
    }

    public async Task<TReadDto> AddAsync(TCreateDto dto) {

        // Cria entidade de id 0, que será sobrescrito pelo banco
        var entity = mapper.ToEntity(dto, 0);
        entity = await _repository.AddAsync(entity);
        return mapper.ToReadDto(entity);
    }

    public async Task<TReadDto?> UpdateAsync(int id, TCreateDto dto) {

        var entity = mapper.ToEntity(dto, id);
        entity = await _repository.UpdateAsync(entity);
        
        if (entity == null) return null;

        return mapper.ToReadDto(entity);
    }

    public async Task<bool> DeleteAsync(int id) {

        return await _repository.DeleteAsync(id);
    }
}