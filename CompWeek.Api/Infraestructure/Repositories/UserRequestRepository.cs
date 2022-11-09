using CompWeek.Api.Domain.Interfaces.Repositories;
using CompWeek.Infrastructure.Contexts;
using CompWeek.Infrastructure.Repositories;

namespace CompWeek.Api.Infraestructure.Repositories;

public class UserRequestRepository : UserRequestRepositoryBase, IUserRequestRepository
{
    public UserRequestRepository(MysqlContext context) : base(context)
    {
    }
}