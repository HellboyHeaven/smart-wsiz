using Application.Attributes;
using Core.Models.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services;

[Service]
public class TokenService(IOptions<JwtSettings> jwtSettings)
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings), "JwtSettings cannot be null.");

    public string GenerateToken(User user)
    {
        var claims = new[]
        {

            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("role", user.Role().ToString())  // Добавляем роль в токен
        };

        System.Diagnostics.Trace.TraceInformation($" hi: {_jwtSettings.Key} {_jwtSettings.Issuer}");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Expire),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public RefreshToken GenerateRefreshToken(User user)
    {
        string value;
        var randomBytes = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
            value = Convert.ToBase64String(randomBytes);
        }

        var refreshToken = new RefreshToken();
        refreshToken.User = user;
        refreshToken.Token = value;
        refreshToken.ExpiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshExpire);
        refreshToken.UserId = user.Id;
        return refreshToken;
    }
}