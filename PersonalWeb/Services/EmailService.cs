using Microsoft.Extensions.Options;
using PersonalWeb.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PersonalWeb.Services
{
    public class EmailService
    {
        private readonly string _from;
        private readonly string _apiKey;

        public EmailService(IOptions<SmtpSettings> smtp)
        {
            _from = smtp.Value.From;
            _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY")
                     ?? throw new Exception("Falta SENDGRID_API_KEY");
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string message)
        {
            var client = new SendGridClient(_apiKey);

            var fromEmail = new EmailAddress(_from);
            var toEmail = new EmailAddress(to);

            var msg = MailHelper.CreateSingleEmail(
                fromEmail,
                toEmail,
                subject,
                plainTextContent: message,
                htmlContent: $"<p>{message}</p>"
            );

            var response = await client.SendEmailAsync(msg);

            return response.IsSuccessStatusCode;
        }
    }
}
