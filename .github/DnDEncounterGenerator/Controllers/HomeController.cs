using DnDEncounterGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DnDClasses;
using static System.Net.Mime.MediaTypeNames;
using DnDClasses.Database;

namespace DnDEncounterGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private User user;

        public HomeController(ILogger<HomeController> logger)
        {
            DbUser dbUser = new();
            user = dbUser.GetUser("test@example.com", "testpassword");
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(user);
        }

        [HttpPost]
        public IActionResult AddPlayer(Player player)
        {
            user.AddPlayer(player);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GenerateCreatures(int maxAmount)
        {
            user.AddEncounter(maxAmount);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}