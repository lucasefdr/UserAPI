using Microsoft.AspNetCore.Authorization;

namespace IdentityAPI.Authorization;

public class MinimumAge : IAuthorizationRequirement
{
    public int YearsOld { get; set; }
    
    public MinimumAge(int yearsOld)
    {
        YearsOld = yearsOld;
    }
}