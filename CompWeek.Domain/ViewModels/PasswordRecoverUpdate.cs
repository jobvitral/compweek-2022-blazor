using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;

namespace CompWeek.Domain.ViewModels;

public class PasswordRecoverUpdate
{
    public string? ValidationKey { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Password { get; set; }

    public ValidationHelper Validate()
    {
        var validation = new ValidationHelper();
        
        if(string.IsNullOrEmpty(ValidationKey))
            validation.AddError(RecoverPasswordError.ValidationKeyIsNullOrEmpty);
        
        if(string.IsNullOrEmpty(DocumentNumber))
            validation.AddError(RecoverPasswordError.DocumentIsNullOrEmpty);
        
        if(string.IsNullOrEmpty(Password))
            validation.AddError(RecoverPasswordError.PasswordIsNullOrEmpty);

        return validation;
    }
}