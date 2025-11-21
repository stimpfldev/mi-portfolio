using SendGrid;
using SendGrid.Helpers.Mail;

namespace PersonalWeb.Services
{
    public class EmailService
    {
        private readonly string _apiKey;

        public EmailService(IConfiguration config)
        {
            _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY")
                     ?? config["SendGrid:ApiKey"];
        }

        public async Task<bool> SendEmailAsync(string fromEmail, string fromName, string messageText)
        {
            var client = new SendGridClient(_apiKey);

            var to = new EmailAddress("federicosdev@gmail.com", "Fede Portfolio");

            // 🔥 FROM válido (usá el que verificaste en SendGrid)
            var from = new EmailAddress("federicosdev@gmail.com", "Portfolio Website");

            var subject = "Nuevo mensaje desde tu sitio web";

            var body = $"Nombre: {fromName}\nEmail: {fromEmail}\nMensaje:\n{messageText}";

            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                body,
                body
            );

            var response = await client.SendEmailAsync(msg);

            return response.IsSuccessStatusCode;
        }
    }
}
