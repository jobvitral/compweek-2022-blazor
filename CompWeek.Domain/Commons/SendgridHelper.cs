using SendGrid;
using SendGrid.Helpers.Mail;

namespace CompWeek.Domain.Commons;

public class SendgridHelper
{
    public string? ApiKey { get; set; }
    public string? FromEmail { get; set; }
    public string? FromName { get; set; }
    public string? ToEmail { get; set; }
    public string? ToName { get; set; }
    public string? Subject { get; set; }
    public string? PlainTextContent { get; set; }
    public string? HtmlContent { get; set; }
    
    public async Task<Response> SendEmail()
    {
        var client = new SendGridClient(ApiKey);
        var from = new EmailAddress(FromEmail, FromName);
        var subject = Subject;
        var to = new EmailAddress(ToEmail, ToName);
        var plainTextContent = PlainTextContent;
        var htmlContent = HtmlContent;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        return response;
    }
}
