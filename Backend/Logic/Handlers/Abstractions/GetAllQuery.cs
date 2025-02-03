using Core.Interfaces;
using System.Linq.Expressions;

namespace Application.Handlers.Abstractions;
public record GetAllQuery<TEntity>(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null) : IQuery<IEnumerable<TEntity>> where TEntity : class, new();
