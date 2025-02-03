using Application.Attributes;
using Application.Services;
using Core.Interfaces;
using Core.Models.Users;

namespace Application.Handlers.Auth;

public record LoginQuery(string Login, string Password) : IQuery<LoginResponse>;
public record LoginResponse(string AccessToken, string RefreshToken);

[Service]
public class Login(
    TokenService tokenService,
    IRepository<User> userRepository,
    IRepository<RefreshToken> refreshTokenRepository) : IQueryHandler<LoginQuery, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginQuery command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(filter: u => u.Login == command.Login);

        if (user == null || !PasswordHasher.VerifyPassword(user.PasswordHash, command.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // Генерация JWT токена
        var jwtToken = tokenService.GenerateToken(user);

        // Генерация Refresh токена
        var newRefreshToken = tokenService.GenerateRefreshToken(user);

        await refreshTokenRepository.AddOrUpdateAsync(newRefreshToken, r => r.User == user, cancellationToken);


        return new LoginResponse(jwtToken, newRefreshToken.Token);
    }


}
