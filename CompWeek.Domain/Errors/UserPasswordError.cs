namespace CompWeek.Domain.Errors;

public class UserPasswordError
{
    public static string UserIsNull => "Usuário é obrigatorio";
    public static string PasswordIsNullOrEmpty => "Usuário é obrigatorio";
}