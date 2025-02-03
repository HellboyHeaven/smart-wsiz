using Application.Attributes;
using Application.Services;
using Core.Interfaces;
using Core.Models.Users;
using Microsoft.EntityFrameworkCore;


namespace Application.Handlers.Auth;



public record RefreshTokenQuery(string Token) : IQuery<LoginResponse>;
[Service]
public class Refresh(TokenService tokenService, IRepository<RefreshToken> refreshTokenRepository) : IQueryHandler<RefreshTokenQuery, LoginResponse>
{
    public async Task<LoginResponse> Handle(RefreshTokenQuery query, CancellationToken cancellationToken = default)
    {

        var oldRefreshToken = await refreshTokenRepository.GetAsync(
            filter: r => r.Token == query.Token,
            includes: q => q.Include(r => r.User)
            );
        Console.WriteLine("tring");
        if (oldRefreshToken == null)
            throw new UnauthorizedAccessException();
        var user = oldRefreshToken.User;

        if (user == null)
        {
            throw new InvalidDataException($"No such user");
        }

        var newRefreshToken = tokenService.GenerateRefreshToken(user!);
        var accessToken = tokenService.GenerateToken(user!);

        newRefreshToken.Id = oldRefreshToken.Id;
        await refreshTokenRepository.UpdateAsync(newRefreshToken);


        return new LoginResponse(accessToken, newRefreshToken.Token);

    }
}
