using CompWeek.Api.Domain.Interfaces.Repositories;
using CompWeek.Infrastructure.Contexts;
using CompWeek.Infrastructure.Repositories;

namespace CompWeek.Api.Infraestructure.Repositories;

public class RoleRepository : RoleRepositoryBase, IRoleRepository
{
    public RoleRepository(MysqlContext context) : base(context)
    {
    }
}