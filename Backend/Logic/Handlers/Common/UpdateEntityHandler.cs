using Application.Attributes;
using Application.Handlers.Abstractions;
using Core.Interfaces;

namespace Application.Handlers.Common;

[Service]
public class UpdateEntityHandler<TEntity>(IRepository<TEntity> repository) : ICommandHandler<UpdateCommand<TEntity>>
    where TEntity : class
{
    public async Task Handle(UpdateCommand<TEntity> command, CancellationToken cancellationToken = default)
    {

        await repository.UpdateAsync(command.Entity, cancellationToken: cancellationToken);
    }
}