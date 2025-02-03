namespace API.Contracts.Requests.Auth;


public class RefreshTokenRequest
{
    public required string RefreshToken { get; set; }
}
