using CompWeek.Api.Domain.Interfaces.Services;

namespace CompWeek.Api.Domain.Interfaces;

public interface IUnitOfService
{
    IAuthenticationService AuthenticationService { get; }
    IPasswordService PasswordService { get; }
    IRoleService RoleService { get; }
    IUserService UserService { get; }
}