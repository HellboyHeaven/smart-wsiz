namespace Persistance;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int Expire { get; set; } // minutes
    public int RefreshExpire { get; set; } // days
}