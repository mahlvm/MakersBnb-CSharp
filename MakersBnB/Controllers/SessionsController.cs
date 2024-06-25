using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MakersBnB.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ILogger<SessionsController> _logger;

        public SessionsController(ILogger<SessionsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("/Sessions/New")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/Sessions")]
        [HttpPost]
        public IActionResult Create(string email, string password)
        {   
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            User? user = dbContext.Users.Where(user => user.Email == email).First();
            if (user!= null && user.Password == password)
            {
                HttpContext.Session.SetInt32("user_id", user.Id);
                return new RedirectResult("/Spaces");
            } else {
                return new RedirectResult("/Sessions/New");
            }
        }


    }
}
