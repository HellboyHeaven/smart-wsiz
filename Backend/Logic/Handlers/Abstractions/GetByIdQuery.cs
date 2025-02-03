using Core.Interfaces;

namespace Application.Handlers.Abstractions;

public record GetByIdQuery<TEntity>(Guid Id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null) : IQuery<TEntity> where TEntity : class, new();
