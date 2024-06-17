using Microsoft.AspNetCore.Mvc;

namespace MakersBnB.Controllers;

public class SpacesController : Controller
{
    private readonly ILogger<SpacesController> _logger;

    public SpacesController(ILogger<SpacesController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.Names = new string[2] { "trevor", "pauline" };
        return View();
    }

}
