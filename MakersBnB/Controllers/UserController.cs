using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MakersBnB.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<SpacesController> _logger;

        public UserController(ILogger<SpacesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }



        [Route("/New")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/Users")]
        [HttpPost]
        public IActionResult Create(User user)
        {   
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        // Here's where we finally use the dbContext
        dbContext.User.Add(user);
        dbContext.SaveChanges();

        // redirect to "/Spaces"
        return new RedirectResult("/Spaces");
        }
    }
}   