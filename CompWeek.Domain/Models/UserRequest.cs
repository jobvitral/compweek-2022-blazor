using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;

namespace CompWeek.Domain.Models;

public class UserRequest
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? ValidationKey { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? ValidatedAt { get; set; }
    
    public User? User { get; set; }
    
    public void ClearDependencies()
    {
        User = null;
    }
    
    public ValidationHelper ValidateInsert()
    {
        var validation = new ValidationHelper();

        if (UserId == null)
        {
            validation.AddError(UserRequestError.UserIsNull);
        }

        if (string.IsNullOrEmpty(ValidationKey))
            ValidationKey = Guid.NewGuid().ToString();

        if (ExpiresAt == null)
            ExpiresAt = DateHelper.GetNow().AddDays(1);

        CreatedAt = DateHelper.GetNow();
        ValidatedAt = null;
        
        return validation;
    }

    public ValidationHelper ValidateUse(User? usuario)
    {
        var validation = new ValidationHelper();

        if (usuario == null)
        {
            validation.AddError(UserRequestError.UserNotFound);
        }

        if(UserId != null)
        {
            if(!string.Equals(User!.Email!.Trim(), usuario!.Email!.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                validation.AddError(UserRequestError.EmailIsIncorrect);
            }
        }
        
        if(string.IsNullOrEmpty(ValidationKey))
            validation.AddError(UserRequestError.ValidationKeyIsNullOrEmpty);
        
        if(ExpiresAt < DateHelper.GetNow())
            validation.AddError(UserRequestError.RequestIsExpired);
        
        if(ValidatedAt != null)
            validation.AddError(UserRequestError.RequestIsValidated);

        return validation;
    }
}
