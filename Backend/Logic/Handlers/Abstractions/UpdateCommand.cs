using Core.Interfaces;

namespace Application.Handlers.Abstractions;

public record UpdateCommand<TEntity>(TEntity Entity) : ICommand;