using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService
{
    private readonly string _apiKey;

    public EmailService()
    {
        _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new Exception("La API KEY de SendGrid no está configurada.");
        }
    }

    public async Task<bool> SendEmailAsync(string fromEmail, string fromName, string messageText)
    {
        var client = new SendGridClient(_apiKey);

        var from = new EmailAddress(fromEmail, fromName);
        var to = new EmailAddress("TU_EMAIL_REAL@gmail.com", "Federico");

        var msg = MailHelper.CreateSingleEmail(from, to, "Nuevo mensaje", messageText, messageText);

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }
}
