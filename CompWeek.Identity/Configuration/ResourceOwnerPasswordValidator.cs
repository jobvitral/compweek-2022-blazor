using System.Security.Claims;
using CompWeek.Identity.Domain.Interfaces;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using IdentityModel;

namespace CompWeek.Identity.Configuration;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly IUnitOfWork _unitOfWork;

    public ResourceOwnerPasswordValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var username = context.UserName;
        var password = context.Password;

        var user = await _unitOfWork.UserRepository.GetByLogin(username, password);
        
        if(user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                new Claim(JwtClaimTypes.Role, user.Role!.Id.ToString()),
                new Claim(JwtClaimTypes.Name, user.Name!),
                new Claim(JwtClaimTypes.Email, user.Email!),
                new Claim(JwtClaimTypes.Scope, user.Role!.Scope!)
            };

            context.Result = new GrantValidationResult(user.Email, "ownerPassword", claims);
        }
        else
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,"Invalid credential");
        }
    }
}
