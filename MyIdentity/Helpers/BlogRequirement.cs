using Microsoft.AspNetCore.Authorization;
using MyIdentity.Models.Dto.Blog;

namespace MyIdentity.Helpers;

public class BlogRequirement : IAuthorizationRequirement
{
}

public class IsBlogForUserAuthorizationHandler : AuthorizationHandler<BlogRequirement, BlogDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BlogRequirement requirement, BlogDto resource)
    {
        if (context.User.Identity?.Name == resource.UserName)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}

