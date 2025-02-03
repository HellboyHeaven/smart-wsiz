using Core.Interfaces;

namespace Application.Handlers.Abstractions;

public record CreateCommand<TEntity>(TEntity Entity) : ICommand;
