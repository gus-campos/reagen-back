
using Microsoft.EntityFrameworkCore;
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Repositories;

public abstract class RepositoryBase<TEntity>(
    AppDbContext context
) : IRepository<TEntity>
    where TEntity : class, IWithId
{
    protected readonly AppDbContext _context = context;

    public async Task<List<TEntity>> GetAllAsync() {

        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id) {

        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity) {

        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        
        var loaded = await GetLoaded(entity);

        return loaded;
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity) {

        var found = await _context.Set<TEntity>().AnyAsync(e => e.Id == entity.Id);
        if (!found) return null;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        var loaded = await GetLoaded(entity);

        return loaded;
    }

    public async Task<bool> DeleteAsync(int id) {

        var item = await _context.Set<TEntity>().FindAsync(id);
        if (item == null) return false;

        _context.Set<TEntity>().Remove(item);
        await _context.SaveChangesAsync();
        
        return true;
    }

    private async Task<TEntity> GetLoaded(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Detached;

        var loaded = await _context.Set<TEntity>()
            .FindAsync(entity.Id);

        if (loaded == null)
            throw new Exception("The loaded entity wasn't expected to be null.");

        return loaded;
    }
}