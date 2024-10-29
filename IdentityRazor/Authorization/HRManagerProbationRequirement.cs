using Microsoft.AspNetCore.Authorization;

namespace IdentityRazor.Authorization
{
    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        public int ProbationMonth { get; }
        public HRManagerProbationRequirement(int probationMonth)
        {
            ProbationMonth = probationMonth;
        }
    }

    public class HRManagerProbationRequirmentHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == "EmploymentDate")) 
            { 
                return Task.CompletedTask;
            }

            if (DateTime.TryParse(context.User.FindFirst(x => x.Type == "EmploymentDate")?.Value, out DateTime employmentDate))
            {
                var period = DateTime.Now - employmentDate;
                if (period.Days > 30 * requirement.ProbationMonth)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
