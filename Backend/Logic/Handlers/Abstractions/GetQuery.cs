using Core.Interfaces;
using System.Linq.Expressions;

namespace Application.Handlers.Abstractions;

public record GetQuery<TEntity>(
    Expression<Func<TEntity, bool>> Filter = null,
    Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null) : IQuery<TEntity> where TEntity : class, new();