using CompWeek.Domain.Filters;
using CompWeek.Domain.Models;

namespace CompWeek.Domain.Interfaces.Repositories;

public interface IUserRepositoryBase : IRepositoryBase<User, int, UserFilter>
{
    Task<User?> GetByLogin(string document, string password);
    Task<bool> CheckEmail(string email, int? userId);
    Task<bool> CheckDocument(string document, int? userId);
}
