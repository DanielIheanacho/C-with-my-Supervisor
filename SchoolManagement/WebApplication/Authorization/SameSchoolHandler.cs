using Microsoft.AspNetCore.Authorization;
using SchoolManagement.Models.ClassModels;

namespace SchoolManagement.Web.Authorization
{
    public class SameSchoolHandler : AuthorizationHandler<SameSchoolRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SameSchoolRequirement requirement,
            User resource)
        {
            var schoolIdClaim = context.User.FindFirst("SchoolId")?.Value;
            
            if (int.TryParse(schoolIdClaim, out int adminSchoolId))
            {
                if (adminSchoolId == resource.SchoolId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
