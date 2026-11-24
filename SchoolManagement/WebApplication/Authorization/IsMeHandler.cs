using Microsoft.AspNetCore.Authorization;
using SchoolManagement.Models.ClassModels;
using System.Security.Claims;

namespace SchoolManagement.Web.Authorization
{
    public class IsMeHandler: AuthorizationHandler<IsMeRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsMeRequirement requirement,
            User resource)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userId, out int intUserId))
            {
                if (intUserId == resource.UserId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
