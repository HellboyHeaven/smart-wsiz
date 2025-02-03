using Application.Attributes;
using Core.Interfaces;
using Core.Models.Users;

namespace Application.Handlers.Auth;

public record LogoutCommand(Guid UserId) : ICommand;

[Service]
public class Logout(IRepository<RefreshToken> refreshTokenRepository) : ICommandHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand command, CancellationToken cancellationToken = default)
    {
        var refreshToken = await refreshTokenRepository.GetAsync(filter: r => r.User.Id == command.UserId);
        await refreshTokenRepository.DeleteAsync(refreshToken);
    }
}
