using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MakersBnB.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ILogger<ReservationsController> _logger;
        private readonly MakersBnBDbContext _dbContext;

        public ReservationsController(ILogger<ReservationsController> logger)
        {
            _logger = logger;
    
        }

        public IActionResult Index()
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            var reservation = dbContext.Reservations.ToList();
            return View(reservation);
        }

        public IActionResult New()
        {
            ViewBag.Spaces = _dbContext.Spaces.ToList();
            ViewBag.Users = _dbContext.Users.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
