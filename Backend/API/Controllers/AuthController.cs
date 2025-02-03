using API.Contracts.Requests.Auth;
using API.Mappings;
using Application.Exceptions;
using Application.Handlers.Auth;
using Core.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;


namespace API.Controllers;

[Authorize]
[Route("api")]
[ApiController]
public class AuthController() : ControllerBase
{
    private UserMapper mapper = new UserMapper();
   



    [HttpGet("verify")]
    public async Task<IActionResult> Verify()
    {
        return Ok("success");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] Contracts.Requests.Auth.LoginRequest request, [FromServices] Login login)
    {
        try
        {
            var command = new LoginQuery(Login: request.Login, Password: request.Password);
            var response = await login.Handle(command);
            return Ok(response);
        }
        catch (UnauthorizedAccessException error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout([FromServices] Logout logoutHandler)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var sub = jwtToken.Subject;

        await logoutHandler.Handle(new LogoutCommand(new Guid(sub)));
        return Ok("success");
    }


    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenQuery refreshTokenCommand, [FromServices] Refresh refresh)
    {
        try
        {
            var response = await refresh.Handle(refreshTokenCommand);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }


}