using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;

namespace CompWeek.Domain.Interfaces.Services;

public interface IUserServiceBase
{
    Task<User?> Get(int param);
    Task<List<User>> Get(UserFilter filter);
    Task<User?> Insert(User entity);
    Task<User?> Update(User entity);
    Task<User?> Delete(int param);
}
