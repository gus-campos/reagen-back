using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;

namespace ReagenBack.Core.Controllers;

[ApiController]
public abstract class CrudControllerBase<TEntity, TCreateDto, TReadDto>(
    ICrudService<TEntity, TCreateDto, TReadDto> crudService
) : ControllerBase 
    where TEntity : class, IWithId
    where TCreateDto : class
    where TReadDto : class, IWithId
{
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TReadDto>>> Get()
    {   
        var readDtos = await crudService.GetAllAsync();
        return Ok(readDtos);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TReadDto>> Get(int id)
    {
        var readDto = await crudService.GetByIdAsync(id);
        if (readDto == null) return NotFound();
        return Ok(readDto);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TReadDto>> Post(TCreateDto createDto)
    {
        var readDto = await crudService.AddAsync(createDto);

        return CreatedAtAction(
            nameof(Get), 
            new { id = readDto.Id }, 
            readDto
        );
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put(int id, TCreateDto createDto)
    {   
        var readDto = await crudService.UpdateAsync(id, createDto);
        if (readDto == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        bool deleted = await crudService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
