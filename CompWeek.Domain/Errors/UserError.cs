namespace CompWeek.Domain.Errors;

public class UserError
{
    public static string RoleIsNull => "Nenhum grupo informado";
    public static string RoleIsZero => "Nenhum grupo informado";
    public static string NameIsNullOrEmpty => "Nome do usuário é obrigatorio";
    public static string PhoneNumberIsNullOrEmpty => "Telefone é obrigatório";
    public static string EmailIsNull => "Email não encontrado";
    public static string EmailIsNullOrEmpty => "Email é obrigatório";
    public static string EmailIsInvalid => "Informe um email válido";
    public static string EmailExists => "Email já está cadastrado";
    public static string PasswordIsNullOrEmpty => "Senha é obrigatória";
    public static string DocumentIsNullOrEmpty => "Documento é obrigatório";
    public static string DocumentExists => "Documento já está cadastrado";
}
