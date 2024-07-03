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
// –––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––//

        [Route("/Spaces")]
        [HttpPost]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public IActionResult Create(Space space)
        {
            // Obtém o UserId da sessão
            int? userId = HttpContext.Session.GetInt32("user_id");
            
            if (userId == null)
            {
                // Se o usuário não está autenticado, redireciona para a página de login
                return new RedirectResult("/Sessions/New");
            }

            space.UserId = userId.Value; // Define o UserId do Space

            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            dbContext.Spaces.Add(space);
            dbContext.SaveChanges();

            return new RedirectResult("/Spaces");
        }

// –––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––//
        
        
        [Route("Spaces/{id}")]
        public IActionResult Details(int id)
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            var space = dbContext.Spaces.Find(id);
            if (space == null)
            {
                return NotFound();
            }

            return View(space);

        }


    }
}
