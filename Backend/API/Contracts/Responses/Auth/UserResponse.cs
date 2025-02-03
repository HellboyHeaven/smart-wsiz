namespace API.Contracts.Responses.Auth;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }
}
