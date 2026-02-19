
namespace ReagenBack.Core.Models;

public interface IWithId
{
    int Id { get; set; }
}

public interface IMapper<TEntity, TCreateDto, TReadDto>
    where TReadDto : IWithId
{
    TEntity ToEntity(TCreateDto dto, int id);
    TReadDto ToReadDto(TEntity entity);
}