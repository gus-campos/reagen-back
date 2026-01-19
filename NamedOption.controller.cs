
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class NamedOptionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public NamedOptionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NamedOption>>> Get()
    {
        return await _context.NamedOptions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NamedOption>> Get(int id)
    {
        var item = await _context.NamedOptions.FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<NamedOption>> Post(NamedOption item)
    {
        _context.NamedOptions.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, NamedOption item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.NamedOptions.FindAsync(id);
        if (item == null) return NotFound();
        _context.NamedOptions.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
