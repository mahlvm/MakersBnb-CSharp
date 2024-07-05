using MakersBnB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MakersBnB.ActionFilters;
using Microsoft.EntityFrameworkCore;
using System;
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

        [HttpGet]
        [Route("/Reservations/New")]
        public IActionResult New(int? spaceId)
        {
            MakersBnBDbContext dbContext = new MakersBnBDbContext();
            var viewModel = new ReservationViewModel
            {
                Spaces = dbContext.Spaces.ToList(),
                Reservation = new Reservation
                {
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow
                }
            };

            if (spaceId.HasValue)
            {
                viewModel.Reservation.SpaceId = spaceId.Value;
            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("/Reservations/Create")]
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

            try
            {
                reservation.StartDate = DateTime.SpecifyKind(reservation.StartDate, DateTimeKind.Utc);
                reservation.EndDate = DateTime.SpecifyKind(reservation.EndDate, DateTimeKind.Utc);

                dbContext.Reservations.Add(reservation);
                dbContext.SaveChanges();

                return RedirectToAction("Confirmation");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("/Reservations/Confirmation")]
        public IActionResult Confirmation()
        {
            return View();
        }

        [HttpGet]
        [Route("/Reservations/Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
