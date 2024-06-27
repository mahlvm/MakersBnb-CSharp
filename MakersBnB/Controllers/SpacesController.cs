using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MakersBnB.ActionFilters;

namespace MakersBnB.Controllers
{
    public class SpacesController : Controller
    {
        private readonly ILogger<SpacesController> _logger;

        public SpacesController(ILogger<SpacesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            var spaces = dbContext.Spaces.ToList();
            return View(spaces);
        }

        [Route("/Spaces/New")]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public IActionResult New()
        {
            return View();
        }

        [Route("/Spaces")]
        [HttpPost]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public IActionResult Create(Space space)
        {   
        MakersBnBDbContext dbContext = new MakersBnBDbContext();
        // Here's where we finally use the dbContext
        dbContext.Spaces.Add(space);
        dbContext.SaveChanges();

        // redirect to "/Spaces"
        return new RedirectResult("/Spaces");
        }
    }
}
