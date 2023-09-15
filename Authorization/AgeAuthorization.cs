using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace IdentityAPI.Authorization;

public class AgeAuthorization : AuthorizationHandler<MinimumAge>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAge requirement)
    {
        var dateOfBirthClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

        if (dateOfBirthClaim is null) return Task.CompletedTask;

        var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value);

        var yearsOld = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth > DateTime.Today.AddYears(-yearsOld))
        {
            yearsOld--;
        }

        if (yearsOld >= requirement.YearsOld) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}