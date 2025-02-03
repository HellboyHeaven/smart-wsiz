using Core.Interfaces;

namespace Application.Handlers.Abstractions;

public record DeleteByIdCommand<TEntity>(Guid Id) : ICommand;