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
            var model = new HomeViewModel
            {
                WelcomeMessage = "Welcome to our team page!",
                Reviews = new List<string> { "Review 1", "Review 2" }
            };
            return View(model);
        }

        public IActionResult Team()
        {
            return View();
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
