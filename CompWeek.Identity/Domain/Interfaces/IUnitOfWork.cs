using CompWeek.Identity.Domain.Interfaces.Repositories;

namespace CompWeek.Identity.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
}