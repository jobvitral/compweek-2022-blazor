using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;

namespace CompWeek.Domain.ViewModels;

public class PasswordRecoverRequest
{
    public string? DocumentNumber { get; set; }
    
    public ValidationHelper Validate()
    {
        var validation = new ValidationHelper();

        if(string.IsNullOrEmpty(DocumentNumber))
            validation.AddError(RecoverPasswordError.DocumentIsNullOrEmpty);

        return validation;
    }
}