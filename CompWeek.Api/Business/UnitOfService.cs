using CompWeek.Api.Business.Services;
using CompWeek.Api.Domain.Interfaces;
using CompWeek.Api.Domain.Interfaces.Services;
using CompWeek.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace CompWeek.Api.Business;

public class UnitOfService : IUnitOfService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOptions<AppSettings> _appSettings;
    private readonly IActionContextAccessor _actionContextAccessor;

    private IAuthenticationService? _authenticationService;
    private IPasswordService? _passwordService;
    private IRoleService? _roleService;
    private IUserService? _userService;

    public UnitOfService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IActionContextAccessor actionContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _appSettings = appSettings;
        _actionContextAccessor = actionContextAccessor;
    }

    public IAuthenticationService AuthenticationService => _authenticationService ??= new AuthenticationService(_unitOfWork, _appSettings, _actionContextAccessor);
    public IPasswordService PasswordService => _passwordService ??= new PasswordService(_unitOfWork, _appSettings);
    public IRoleService RoleService => _roleService ??= new RoleService(_unitOfWork);
    public IUserService UserService => _userService ??= new UserService(_unitOfWork, this);
}