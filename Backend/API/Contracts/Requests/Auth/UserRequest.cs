﻿namespace API.Contracts.Requests.Auth;

[Serializable]
public abstract class UserRequest
{
    public Guid Id { get; set; }
    public required string Login { get; set; }
    public string? Password { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required DateOnly Birthday { get; set; }
    //public required string Role { get; set; }


}