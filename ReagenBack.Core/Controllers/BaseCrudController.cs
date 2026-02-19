using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Controllers;

[ApiController]
public abstract class CrudControllerBase<TEntity, TCreateDto, TReadDto>(
    AppDbContext context,
    IMapper<TEntity, TCreateDto, TReadDto> mapper
) : ControllerBase 
    where TEntity : class, IWithId
    where TReadDto : IWithId
{
    // FIXME: Verificar nomes repetido - na verdade isso é service

    protected readonly AppDbContext _context = context;
    protected readonly IMapper<TEntity, TCreateDto, TReadDto> _mapper = mapper;

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TReadDto>>> Get()
    {   
        var entities = await _context.Set<TEntity>().ToListAsync();
        return Ok(entities.Select(_mapper.ToReadDto));
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TReadDto>> Get(int id)
    {
        var item = await _context.Set<TEntity>().FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TReadDto>> Post(TCreateDto item)
    {
        var entity = _mapper.ToEntity(item, 0);
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(Get), 
            new { id = entity.Id }, 
            _mapper.ToReadDto(entity)
        );
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put(int id, TCreateDto item)
    {   
        var entity = _mapper.ToEntity(item, id);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Set<TEntity>().FindAsync(id);
        if (item == null) return NotFound();

        _context.Set<TEntity>().Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
