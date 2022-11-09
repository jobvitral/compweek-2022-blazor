using System.ComponentModel.DataAnnotations.Schema;
using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;

namespace CompWeek.Domain.Models;

public class User
{
    public int Id { get; set; }
    public int? RoleId { get; set; }
    public string? Name { get; set; }
    public string? Nickname { get; set; }
    public string? DocumentNumber { get; set; }
    public string? DocumentType { get; set; }
    public string? PhoneNumer { get; set; }
    public string? Email { get; set; }
    public string? Thumbnail { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public bool? IsConfirmed { get; set; }
    public bool? IsActive { get; set; }

    public Role? Role { get; set; }
    public ICollection<UserRequest>? Requests { get; set; }
    [NotMapped] public ICollection<UserPassword>? Passwords { get; set; }

    public void ClearDependencies()
    {
        Role = null;
        Requests = null;
        Passwords = null;
    }

    public ValidationHelper ValidateInsert(bool emailCheck, bool documentCheck)
    {
        var validation = new ValidationHelper();

        if (RoleId == null)
            validation.AddError(UserError.RoleIsNull);
        else
            if (RoleId == 0)
                validation.AddError(UserError.RoleIsZero);

        if (string.IsNullOrEmpty(Name))
            validation.AddError(UserError.NameIsNullOrEmpty);

        if (string.IsNullOrEmpty(Nickname))
            Nickname = Name;
        
        if (string.IsNullOrEmpty(PhoneNumer))
            validation.AddError(UserError.PhoneNumberIsNullOrEmpty);
        
        if (string.IsNullOrEmpty(Email))
            validation.AddError(UserError.EmailIsNullOrEmpty);
        else 
            if(!EmailHelper.IsValid(Email))
                validation.AddError(UserError.EmailIsInvalid);
        
        if (string.IsNullOrEmpty(DocumentNumber))
            validation.AddError(UserError.DocumentIsNullOrEmpty);
        
        if(emailCheck)
            validation.AddError(UserError.EmailExists);
        
        if(documentCheck)
            validation.AddError(UserError.DocumentExists);
        
        if(Passwords == null)
            validation.AddError(UserError.PasswordIsNullOrEmpty);
        else if(Passwords.Count == 0)
            validation.AddError(UserError.PasswordIsNullOrEmpty);

        // seta valores padrao nos atributos
        CreatedAt ??= DateHelper.GetNow();
        IsConfirmed ??= false;
        IsActive ??= true;

        return validation;
    }
    
    public ValidationHelper ValidateUpdate(bool emailCheck, bool documentCheck)
    {
        var validation = new ValidationHelper();

        if (RoleId == null)
            validation.AddError(UserError.RoleIsNull);
        else
            if (RoleId == 0)
                validation.AddError(UserError.RoleIsZero);

        if (string.IsNullOrEmpty(Name))
            validation.AddError(UserError.NameIsNullOrEmpty);

        if (string.IsNullOrEmpty(Nickname))
            Nickname = Name;
        
        if (string.IsNullOrEmpty(PhoneNumer))
            validation.AddError(UserError.PhoneNumberIsNullOrEmpty);
        
        if (string.IsNullOrEmpty(Email))
            validation.AddError(UserError.EmailIsNullOrEmpty);
        else 
        if(!EmailHelper.IsValid(Email))
            validation.AddError(UserError.EmailIsInvalid);
        
        if(emailCheck)
            validation.AddError(UserError.EmailExists);
        
        if(documentCheck)
            validation.AddError(UserError.DocumentExists);
        
        return validation;
    }
}
