using CompWeek.Domain.Commons;
using CompWeek.Domain.Errors;

namespace CompWeek.Domain.Models;

public class Role
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Scope { get; set; }
    public bool? IsDefault { get; set; }
    public ICollection<User>? Users { get; set; }

    public void ClearDependencies()
    {
        Users = null;
    }

    public ValidationHelper Validate()
    {
        var validation = new ValidationHelper();

        if (string.IsNullOrEmpty(Name))
        {
            validation.IsValid = false;
            validation.Errors.Add(RoleError.NameIsNullOrEmpty);
        }
        
        if (string.IsNullOrEmpty(Scope))
        {
            validation.IsValid = false;
            validation.Errors.Add(RoleError.ScopeIsNullOrEmpty);
        }
        
        IsDefault ??= false;

        return validation;
    }
}
