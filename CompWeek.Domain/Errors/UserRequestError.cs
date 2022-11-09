namespace CompWeek.Domain.Errors;

public class UserRequestError
{
    public static string RequestIsNull => "Solicitação não encontrada";
    public static string UserIsNull => "Usuário é obrigatorio";
    public static string UserNotFound => "Usuário não encontrado";
    public static string ValidationKeyIsNullOrEmpty => "Chave é obrigatório";
    public static string RequestIsExpired => "Registro está expirado";
    public static string RequestIsValidated => "Registro já foi utilizado";
    public static string EmailIsIncorrect => "Email informado é incorreto";
}
