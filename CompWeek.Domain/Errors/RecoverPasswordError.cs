namespace CompWeek.Domain.Errors;

public class RecoverPasswordError
{
    public static string DocumentIsNullOrEmpty => "CPF/CNPJ ou Email é obrigatorio";
    public static string PasswordIsNullOrEmpty => "Nova senha é obrigatória";
    public static string ValidationKeyIsNullOrEmpty => "Chave da solicitação é obrigatório";
}
