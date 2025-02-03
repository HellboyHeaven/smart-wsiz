using Application.Attributes;
using Application.Handlers.Abstractions;
using Core.Interfaces;

namespace Application.Handlers.Common;

[Service]
public class GetAllEntitiesHandler<TEntity>(IRepository<TEntity> repository) : IQueryHandler<GetAllQuery<TEntity>, IEnumerable<TEntity>>
    where TEntity : class, new()
{
    public virtual async Task<IEnumerable<TEntity>> Handle(GetAllQuery<TEntity> query, CancellationToken cancellationToken = default)
    {
        return await repository.GetAllAsync(filter: query.filter, includes: query.includes, cancellationToken: cancellationToken);
    }
}


