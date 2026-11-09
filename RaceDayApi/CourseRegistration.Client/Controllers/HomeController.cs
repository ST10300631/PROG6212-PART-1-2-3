using Microsoft.AspNetCore.Mvc;
using RaceDaySystem.Client.Models;
using RaceDaySystem.Client.Models.DTOs;
using RaceDaySystem.Client.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RaceDaySystem.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RaceDayApiService _apiService;

        // Merged single constructor that resolves both dependencies
        public HomeController(ILogger<HomeController> logger, RaceDayApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
