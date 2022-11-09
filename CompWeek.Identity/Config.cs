using Duende.IdentityServer.Models;

namespace CompWeek.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(name: "administrator", displayName: "Administrator"),
            new ApiScope(name: "company", displayName: "Company"),
            new ApiScope(name: "seller", displayName: "Seller"),
            new ApiScope(name: "buyer", displayName: "Buyer"),
        };
}