namespace CompWeek.Domain.Errors;

public class RoleError
{
    public static string NameIsNullOrEmpty => "Nome é obrigatorio";
    public static string ScopeIsNullOrEmpty => "Escopo é obrigatorio";
}
