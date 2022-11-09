using CompWeek.Api.Domain.Interfaces.Repositories;
using CompWeek.Infrastructure.Contexts;
using CompWeek.Infrastructure.Repositories;

namespace CompWeek.Api.Infraestructure.Repositories;

public class UserRepository : UserRepositoryBase, IUserRepository
{
    public UserRepository(MysqlContext context) : base(context)
    {
    }
}