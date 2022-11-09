using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;

namespace CompWeek.Domain.Interfaces.Repositories;

public interface IUserPasswordRepositoryBase : IRepositoryBase<UserPassword, int, UserPasswordFilter>
{
    Task<List<UserPassword>> GetByUser(int userId);
    Task<UserPassword?> GetCurrent(int userId);
    Task<UserPassword?> Update(int userId, string newPassword);
}