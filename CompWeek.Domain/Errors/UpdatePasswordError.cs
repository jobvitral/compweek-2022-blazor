namespace CompWeek.Domain.Errors;

public class UpdatePasswordError
{
    public static string DocumentIsNullOrEmpty => "CPF/CNPJ ou Email é obrigatorio";
    public static string CurrentPasswordIsNullOrEmpty => "Senha atual é obrigatória";
    public static string NewPasswordIsNullOrEmpty => "Nova senha é obrigatória";
    public static string LoginIsNull => "Senha atual informada é inválida";
}
