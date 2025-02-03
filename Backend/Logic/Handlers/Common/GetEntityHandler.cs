using Application.Attributes;
using Application.Handlers.Abstractions;
using Core.Interfaces;

namespace Application.Handlers.Common;

[Service]
public class GetEntityHandler<TEntity>(IRepository<TEntity> repository) : IQueryHandler<GetQuery<TEntity>, TEntity>
    where TEntity : class, new()
{
    public async Task<TEntity> Handle(GetQuery<TEntity> query, CancellationToken cancellationToken = default)
    {
        return await repository.GetAsync(cancellationToken);
    }
}
