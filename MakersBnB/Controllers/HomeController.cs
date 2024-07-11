using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MakersBnB.Models;

namespace MakersBnB.Controllers
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
            
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                
                return Redirect("/Spaces");
            }
            else
            {
                
                return Redirect("/Sessions/New");
            }
        }

        [Route("/Home/Logout")]
        public IActionResult Logout()
        {
            
            HttpContext.Session.Clear();

            
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
