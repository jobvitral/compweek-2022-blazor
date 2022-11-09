using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;

namespace CompWeek.Domain.ViewModels;

public class PasswordUpdateRequest
{
    public string? DocumentNumber { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }

    public ValidationHelper Validate()
    {
        var validation = new ValidationHelper();
        
        if(string.IsNullOrEmpty(DocumentNumber))
            validation.AddError(UpdatePasswordError.DocumentIsNullOrEmpty);
        
        if(string.IsNullOrEmpty(CurrentPassword))
            validation.AddError(UpdatePasswordError.CurrentPasswordIsNullOrEmpty);
        
        if(string.IsNullOrEmpty(NewPassword))
            validation.AddError(UpdatePasswordError.NewPasswordIsNullOrEmpty);

        return validation;
    }
}