namespace CompWeek.Web.Domain.Models;

public class PageToken
{
    public static string Autehentication => "/authentication";
    public static string AutehenticationForgotPassword => "/authentication/forgot_password";
    public static string AutehenticationRecoverPassword => "/authentication/recover_password/{0}";
}