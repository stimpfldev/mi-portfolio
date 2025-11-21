using Microsoft.Extensions.Options;
using PersonalWeb.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PersonalWeb.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _settings;

        public EmailService(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.User, _settings.Pass),
                EnableSsl = _settings.EnableSsl
            };

            var mail = new MailMessage()
            {
                From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                Subject = subject,
                Body = body
            };

            mail.To.Add(to);

            await client.SendMailAsync(mail);
        }
    }
}
