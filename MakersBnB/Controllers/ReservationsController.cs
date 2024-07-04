using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MakersBnB.ActionFilters;

namespace MakersBnB.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ILogger<SpacesController> _logger;

        public ReservationsController(ILogger<SpacesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            var reservations = dbContext.Reservations.ToList();
            return View(reservations);
        }

        [Route("/Reservations/New")]
        public IActionResult New()
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            ViewBag.Spaces = dbContext.Spaces.ToList();
            ViewBag.Users = dbContext.Users.ToList();
            return View();
        }
// –––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––//

        [Route("/Reservations")]
        [HttpPost]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public IActionResult Create(Reservation reservation)
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();

            
            reservation.StartDate = DateTime.SpecifyKind(reservation.StartDate, DateTimeKind.Utc);
            reservation.EndDate = DateTime.SpecifyKind(reservation.EndDate, DateTimeKind.Utc);

            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();

            return new RedirectResult("Index");


        }

// –––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––//
        
        


    }
}
