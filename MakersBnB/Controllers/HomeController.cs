using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MakersBnB.Models;

namespace MakersBnB.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var model = new HomeViewModel
            {
                WelcomeMessage = "Welcome to MakersBnB",
                Reviews = new List<string>
                {
                    "Great place to stay!",
                    "Test",
                    "Had an amazing time!",
                    "Highly recommend this property."
                }
            };
        return View(model);
    }
}
