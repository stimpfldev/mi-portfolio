using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalWeb.Models;
using System.Net;
using System.Net.Mail;
namespace PersonalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // 🧱 Nuevo: Acción GET (mostrar formulario)
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // 🧱 Nuevo: Acción POST (enviar correo)
        [HttpPost]
        public IActionResult Contact(ContactModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential("federicosdev@gmail.com", "bbli kfal yoko xytj");
                    client.EnableSsl = true;

                    var mail = new MailMessage();
                    mail.From = new MailAddress("federicosdev@gmail.com");
                    mail.To.Add("federicosdev@gmail.com");
                    mail.Subject = "Nuevo mensaje desde el sitio web";
                    mail.Body = $"Nombre: {model.Name}\nCorreo: {model.Email}\nMensaje:\n{model.Message}";

                    client.Send(mail);
                }

                ViewBag.Success = "✅ Tu mensaje fue enviado correctamente.";
                ModelState.Clear();
            }
            catch (Exception)
            {
                ViewBag.Message = "We couldn’t send your message. Please check your email configuration or try again later.";
            }

            return View();
        }
    }
}