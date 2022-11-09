using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;

namespace CompWeek.Domain.ViewModels;

public class AuthenticationRequest
{
    public string? DocumentNumber { get; set; }
    public string? Password { get; set; }

    public ValidationHelper Validate()
    {
        var validation = new ValidationHelper();

        if(string.IsNullOrEmpty(DocumentNumber))
            validation.AddError(AuthenticationError.DocumentIsNullOrEmpty);

        if(string.IsNullOrEmpty(Password))
            validation.AddError(AuthenticationError.PasswordIsNullOrEmpty);

        return validation;
    }
}