using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MakersBnB.ActionFilters;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

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
        public IActionResult Create(Space space, IFormFile Photo)
        {
            // Obtém o UserId da sessão
            int? userId = HttpContext.Session.GetInt32("user_id");
            
            if (userId == null)
            {
                // Se o usuário não está autenticado, redireciona para a página de login
                return new RedirectResult("/Sessions/New");
            }

            if (Photo != null && Photo.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Photo.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Certifique-se de que a pasta de uploads existe
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

                space.PhotoPath = "/uploads/" + uniqueFileName;
            }

            space.UserId = userId.Value; // Define o UserId do Space

            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            dbContext.Spaces.Add(space);
            dbContext.SaveChanges();

            return new RedirectResult("/Spaces");
        }

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
