using CompWeek.Domain.Models;
using CompWeek.Domain.ViewModels;

namespace CompWeek.Domain.Interfaces.Services;

public interface IPasswordServiceBase
{
    Task<User?> Update(PasswordUpdateRequest request);
    Task<UserRequest?> RequestRecovery(PasswordRecoverRequest item);
    Task<UserRequest?> Recover(PasswordRecoverUpdate item);
}
