namespace CompWeek.Domain.Errors;

public class AuthenticationError
{
    public static string DocumentIsNullOrEmpty => "Documento ou email é obrigatorio";
    public static string PasswordIsNullOrEmpty => "Senha é obrigatorio";
    public static string UserIsNull => "Usuário ou senha incorreto";
}
