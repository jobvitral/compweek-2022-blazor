using CompWeek.Domain.ViewModels;

namespace CompWeek.Domain.Interfaces.Services;

public interface IAuthenticationServiceBase
{
    Task<AuthenticationResponse?> Authenticate(AuthenticationRequest request);
}
