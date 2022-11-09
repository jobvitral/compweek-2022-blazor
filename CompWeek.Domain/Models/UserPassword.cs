using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;

namespace CompWeek.Domain.Models;

public class UserPassword
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Password { get; set; }
    public string? RemindTip { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? InactivatedAt { get; set; }
    public bool IsActive { get; set; }

    public User? User { get; set; }

    public void ClearDependencies()
    {
        User = null;
    }
    
    public ValidationHelper Validate(bool validateUser)
    {
        var validation = new ValidationHelper();

        if (validateUser)
        {
            if (UserId == 0)
                validation.AddError(UserPasswordError.UserIsNull);
        }

        if (string.IsNullOrEmpty(Password))
            validation.AddError(UserPasswordError.PasswordIsNullOrEmpty);
        
        CreatedAt ??= DateHelper.GetNow();
        
        return validation;
    }
}