using Microsoft.AspNetCore.Mvc;
using PersonalWeb.Models;
using PersonalWeb.Services;
using System.Diagnostics;
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
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Contact(ContactModel model, [FromServices] EmailService emailService)
        {
            if (!ModelState.IsValid)
                return View(model);

            var ok = await emailService.SendEmailAsync(
                model.Email,
                model.Name,
                model.Message
            );

            if (ok)
            {
                ViewBag.Success = "Your message has been sent!";
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "We couldn't send your message. Try again later.";
            }

            return View();
        }

    }
}