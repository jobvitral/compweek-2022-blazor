using CompWeek.Domain.Interfaces.Services;
using CompWeek.Domain.Models;
using SendGrid;

namespace CompWeek.Api.Domain.Interfaces.Services;

public interface IPasswordService : IPasswordServiceBase
{
    Task<Response?> SendMail(UserRequest? item);
}