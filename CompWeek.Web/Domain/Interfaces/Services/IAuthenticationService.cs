using CompWeek.Domain.Interfaces.Services;
using CompWeek.Domain.ViewModels;
using CompWeek.Web.Domain.Models;

namespace CompWeek.Web.Domain.Interfaces.Services;

public interface IAuthenticationService : IAuthenticationServiceBase
{
    Task<Credential?> GetCurrent();
    Task WriteToStorage(AuthenticationResponse? item);
    Task ClearStorage();
    Task<AuthenticationResponse?> ReadFromStorage();
}