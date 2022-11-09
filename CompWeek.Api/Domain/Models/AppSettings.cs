namespace CompWeek.Api.Domain.Models;

public class AppSettings
{
    public string? IdentityServer { get; set; }
    public SendGridSettings? Sendgrid { get; set; }
}