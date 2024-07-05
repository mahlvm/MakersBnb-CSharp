using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MakersBnB.ActionFilters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MakersBnB.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(ILogger<ReservationsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            var reservations = dbContext.Reservations
                                        .Include(r => r.Space)
                                        .Include(r => r.User)
                                        .ToList();
            return View(reservations);
        }

        [Route("/Reservations/New")]
        public IActionResult New()
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            var viewModel = new ReservationViewModel
            {
                Spaces = dbContext.Spaces.ToList()
            };
            return View(viewModel);
        }

        [Route("/Reservations")]
        [HttpPost]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public IActionResult Create(ReservationViewModel viewModel)
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();

            // Obtém o UserId da sessão
            int? userId = HttpContext.Session.GetInt32("user_id");

            if (userId == null)
            {
                // Se o usuário não está autenticado, redireciona para a página de login
                return RedirectToAction("New", "Sessions");
            }

            var reservation = viewModel.Reservation;
            reservation.UserId = userId.Value;

            // Define o SpaceId da reserva
            var space = dbContext.Spaces.Find(reservation.SpaceId);
            if (space == null)
            {
                // Se o espaço não existe, redireciona para a página de criação de reserva com erro
                ModelState.AddModelError("", "Invalid space.");
                var newViewModel = new ReservationViewModel
                {
                    Spaces = dbContext.Spaces.ToList(),
                    Reservation = reservation
                };
                return View("New", newViewModel);
            }

            reservation.StartDate = DateTime.SpecifyKind(reservation.StartDate, DateTimeKind.Utc);
            reservation.EndDate = DateTime.SpecifyKind(reservation.EndDate, DateTimeKind.Utc);

            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
