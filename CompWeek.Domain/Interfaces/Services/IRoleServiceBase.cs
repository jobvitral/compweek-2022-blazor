using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;

namespace CompWeek.Domain.Interfaces.Services;

public interface IRoleServiceBase
{
    Task<Role?> Get(int param);
    Task<List<Role>> Get(RoleFilter filter);
    Task<Role?> Insert(Role entity);
    Task<Role?> Update(Role entity);
    Task<Role?> Delete(int param);
}
