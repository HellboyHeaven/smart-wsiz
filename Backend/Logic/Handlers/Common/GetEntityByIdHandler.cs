using Application.Attributes;
using Application.Handlers.Abstractions;
using Core.Interfaces;

namespace Application.Handlers.Common;

[Service]
public class GetEntityByIdHandler<TEntity>(IRepository<TEntity> repository) : IQueryHandler<GetByIdQuery<TEntity>, TEntity>
    where TEntity : class, new()
{

    public async Task<TEntity> Handle(GetByIdQuery<TEntity> query, CancellationToken cancellationToken = default)
    {
        return await repository.GetByIdAsync(query.Id, includes: query.includes, cancellationToken: cancellationToken);
    }
}



