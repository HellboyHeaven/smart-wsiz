using System.Linq.Expressions;

namespace Core.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, Func<IQueryable<T>, IQueryable<T>>? includes = null, CancellationToken cancellationToken = default);
    Task<T?> GetAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddAllAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddOrUpdateAsync(T entity, Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>>? includes = null, bool tracked = true, CancellationToken cancellationToken = default);
    Task<T?> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>>? includes = null, bool tracked = true, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>>? includes = null, bool tracked = true, CancellationToken cancellationToken = default);
}
