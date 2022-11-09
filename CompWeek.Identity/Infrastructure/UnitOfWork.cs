using CompWeek.Identity.Domain.Interfaces;
using CompWeek.Identity.Domain.Interfaces.Repositories;
using CompWeek.Identity.Infrastructure.Repositories;
using CompWeek.Infrastructure.Contexts;

namespace CompWeek.Identity.Infrastructure;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MysqlContext _context;
    
    private IUserRepository? _userRepository;
    
    public UnitOfWork(IConfiguration configuration)
    {
        _context = new MysqlContext(configuration);
    }
    
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
    
    public void Dispose()
    {
        _context.Dispose();
    }
}