using Application.Services;
using Infrastructure.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Infrastructure.AuthHandlers;

public class UserRoleHandler(UserService userService) : AuthorizationHandler<UserRoleRequirement>
{


    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRoleRequirement requirement)
    {

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
       
        if (userId == null)
        {
            context.Fail();
            return;
        }


        var user = await userService.GetByIdAsync(new Guid(userId));
       
        if (user != null && (requirement.Roles.Count() == 0 || requirement.Roles.Contains(user.Role())))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}

