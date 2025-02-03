using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistance.Repositories;

public class Repository<T>(UniversityDbContext context) : IRepository<T> where T : class
{
    protected readonly UniversityDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    protected virtual Func<IQueryable<T>, IQueryable<T>> Included {  get; }

    public virtual async Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyQuery(includes: includes ?? Included);
        return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyQuery(includes: includes ?? Included);
        return await query.Where(e => ids.Contains(EF.Property<Guid>(e, "Id"))).ToListAsync();
    }
    public virtual async Task<T?> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }




    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var added= await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync();
        return added.Entity;
    }
    public virtual async Task AddAllAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(T entity, Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        
        var existingEntity = filter == null ? await _dbSet.FindAsync(typeof(T).GetProperty("Id").GetValue(entity)) : await GetAsync(filter);
        if (existingEntity != null)
        {
            // Entity exists, so update it
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            throw new DbUpdateException($"No exist {typeof(T)}");
        }
        _dbSet.Update(existingEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove( await _dbSet.FindAsync(id));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task AddOrUpdateAsync(T entity, Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        try
        {
            UpdateAsync(entity, filter);
        }
        catch
        {
            await _context.Set<T>().AddAsync(entity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<T?> GetSingleAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IQueryable<T>>? includes = null,
        bool tracked = true,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyQuery(filter, includes ?? Included, tracked);
        return await query.SingleOrDefaultAsync(cancellationToken);
    }


    public async Task<T?> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IQueryable<T>>? includes = null,
        bool tracked = true,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyQuery(filter, includes ?? Included, tracked);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IQueryable<T>>? includes = null,
        bool tracked = true,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyQuery(filter, includes ?? Included, tracked);
        return await query.ToListAsync(cancellationToken);
    }

    private IQueryable<T> ApplyQuery(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IQueryable<T>>? includes = null,
        bool tracked = true)
    {
        IQueryable<T> query = _context.Set<T>();
        query = AplyIncludes(query, includes);
        query = ApplyFilters(query, filter);

        if (!tracked)
            query = query.AsNoTracking();

        return query;
    }


    private IQueryable<T> AplyIncludes(IQueryable<T> query, Func<IQueryable<T>, IQueryable<T>> includes)
    {
        if (includes == null)
            return query;
        return includes(query);
    }

    private IQueryable<T> ApplyFilters(IQueryable<T> query, Expression<Func<T, bool>> filter)
    {
        if (filter == null)
            return query;

        return query.Where(filter);
    }


}
