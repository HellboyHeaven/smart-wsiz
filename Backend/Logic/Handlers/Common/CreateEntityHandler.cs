using Application.Attributes;
using Application.Handlers.Abstractions;
using Core.Interfaces;

namespace Application.Handlers.Common;

[Service]
public class CreateEntityHandler<TEntity>(IRepository<TEntity> repository) : ICommandHandler<CreateCommand<TEntity>>
    where TEntity : class
{
    public async Task Handle(CreateCommand<TEntity> command, CancellationToken cancellationToken = default)
    {
        await repository.AddAsync(command.Entity, cancellationToken);
    }
}
