using CompWeek.Api.Domain.Interfaces;
using CompWeek.Api.Domain.Interfaces.Repositories;
using CompWeek.Api.Infraestructure.Repositories;
using CompWeek.Infrastructure.Contexts;

namespace CompWeek.Api.Infraestructure;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MysqlContext _context;
    
    private IRoleRepository? _roleRepository;
    private IUserRepository? _userRepository;
    private IUserPasswordRepository? _userPasswordRepository;
    private IUserRequestRepository? _userRequestRepository;
    
    public UnitOfWork(IConfiguration configuration)
    {
        _context = new MysqlContext(configuration);
    }

    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_context);
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
    public IUserPasswordRepository UserPasswordRepository => _userPasswordRepository ??= new UserPasswordRepository(_context);
    public IUserRequestRepository UserRequestRepository => _userRequestRepository ??= new UserRequestRepository(_context);

    public void Dispose()
    {
        _context.Dispose();
    }
}