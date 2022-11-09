using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace CompWeek.Identity.Configuration;

public class ProfileService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        context.IssuedClaims = context.Subject.Claims.ToList();
            
        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}