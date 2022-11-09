using CompWeek.Api.Domain.Interfaces.Repositories;

namespace CompWeek.Api.Domain.Interfaces;

public interface IUnitOfWork
{
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    IUserPasswordRepository UserPasswordRepository { get; }
    IUserRequestRepository UserRequestRepository { get; }
}