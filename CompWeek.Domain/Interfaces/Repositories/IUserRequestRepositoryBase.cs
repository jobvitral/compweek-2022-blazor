using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;

namespace CompWeek.Domain.Interfaces.Repositories;

public interface IUserRequestRepositoryBase : IRepositoryBase<UserRequest, int, UserRequestFilter>
{
    Task<UserRequest?> GetByKey(string key);
}
