using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public interface IWithId
{
    int Id { get; set; }
}

[ApiController]
public abstract class CrudControllerBase<TEntity> : ControllerBase where TEntity : class, IWithId
{
    protected readonly AppDbContext _context;
    protected CrudControllerBase(AppDbContext context) => _context = context;

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get() =>
        await _context.Set<TEntity>().ToListAsync();

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity>> Get(int id)
    {
        var item = await _context.Set<TEntity>().FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Post(TEntity item)
    {
        _context.Set<TEntity>().Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put(int id, TEntity item)
    {
        if (item.Id != id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
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
