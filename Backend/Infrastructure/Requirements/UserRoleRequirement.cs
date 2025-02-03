using Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Requirements;

public class UserRoleRequirement(params UserRole[] userRoles) : IAuthorizationRequirement
{
    public UserRole[] Roles { get; } = userRoles;

}

