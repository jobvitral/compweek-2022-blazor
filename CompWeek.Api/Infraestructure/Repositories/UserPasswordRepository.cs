using CompWeek.Api.Domain.Interfaces.Repositories;
using CompWeek.Infrastructure.Contexts;
using CompWeek.Infrastructure.Repositories;

namespace CompWeek.Api.Infraestructure.Repositories;

public class UserPasswordRepository : UserPasswordRepositoryBase, IUserPasswordRepository
{
    public UserPasswordRepository(MysqlContext context) : base(context)
    {
    }
}