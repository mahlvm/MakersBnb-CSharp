using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MakersBnB.Models;

namespace MakersBnB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Certifique-se de que há apenas um construtor com parâmetros que o DI pode resolver
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Verifica se o usuário está autenticado
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                // Se autenticado, redireciona para a página de listagem de espaços
                return Redirect("/Spaces");
            }
            else
            {
                // Se não autenticado, redireciona para a página de login
                return Redirect("/Sessions/New");
            }
        }

        [Route("/Home/Logout")]
        public IActionResult Logout()
        {
            // Limpa a sessão
            HttpContext.Session.Clear();

            // Redireciona para a página inicial
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Team()
        {
            return View();
        }

        public IActionResult CreateAccount()
        {

            return Redirect("/Users/New");
        }


        public IActionResult Contactus()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
