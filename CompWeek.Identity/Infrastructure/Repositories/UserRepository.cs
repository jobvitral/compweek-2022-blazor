using CompWeek.Identity.Domain.Interfaces.Repositories;
using CompWeek.Infrastructure.Contexts;
using CompWeek.Infrastructure.Repositories;

namespace CompWeek.Identity.Infrastructure.Repositories;

public class UserRepository : UserRepositoryBase, IUserRepository
{
    public UserRepository(MysqlContext context) : base(context)
    {
    }
}