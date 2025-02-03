using Application.Services;
using Infrastructure.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Infrastructure.AuthHandlers;
internal class AuthorizeHandler(UserService userService) : AuthorizationHandler<AuthorizeRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine(userId);
        if (userId == null)
        {
            context.Fail();
            return;
        }

        var user = await userService.GetByIdAsync(new Guid(userId));
        if (user == null)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
