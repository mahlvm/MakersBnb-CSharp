using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MakersBnB.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // ViewBag.Space = new Space("a house", "a house in a place", 100);
            return View();
        }



        // [Route("/Users/New")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/Users")]
        [HttpPost]
        public IActionResult Create(User user)
        {   
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
       
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

    
        return new RedirectResult("/Spaces");
        }
    }
}   
