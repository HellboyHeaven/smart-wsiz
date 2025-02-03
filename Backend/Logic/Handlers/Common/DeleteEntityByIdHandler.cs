using Application.Attributes;
using Application.Handlers.Abstractions;
using Core.Interfaces;

namespace Application.Handlers.Common;

[Service]
public class DeleteEntityByIdHandler<TEntity>(IRepository<TEntity> repository) : ICommandHandler<DeleteByIdCommand<TEntity>>
    where TEntity : class, new()
{

    public async Task Handle(DeleteByIdCommand<TEntity> query, CancellationToken cancellationToken = default)
    {
        await repository.DeleteByIdAsync(query.Id, cancellationToken: cancellationToken);
    }
}



