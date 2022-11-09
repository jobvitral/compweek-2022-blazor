using CompWeek.Domain.Commons;
using CompWeek.Domain.Models;

namespace CompWeek.Domain.ViewModels;

public class AuthenticationResponse
{
    public int Id { get; set; }
    public DateTime? Expires { get; set; }
    public string? Scope { get; set; }
    public string? Token { get; set; }

    public AuthenticationResponse()
    {
        
    }

    public AuthenticationResponse(User user, string token, int expiresIn)
    {
        Id = user.Id;
        Expires = DateHelper.GetNow().AddSeconds(expiresIn);
        Scope = user.Role!.Scope;
        Token = token;
    }
}