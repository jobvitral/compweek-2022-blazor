using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;

namespace CompWeek.Domain.Interfaces.Repositories;

public interface IRoleRepositoryBase : IRepositoryBase<Role, int, RoleFilter>
{
}
