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

        public async Task<IActionResult> Index()
        {
            // Gather all data elements concurrently from your API service
            var eventsData = await _apiService.GetEventsAsync();
            var categoriesData = new List<CategoryDto>();
            var resultsData = new List<ResultDto>();

            // Pack them completely into the master layout model bundle
            var dashboardBundle = new DashboardViewModel
            {
                Categories = categoriesData,
                Results = resultsData,
                Events = eventsData,

                // FIX: Satisfy all required properties of CreateEventDto
                NewEvent = new CreateEventDto
                {
                    Name = string.Empty,
                    Description = string.Empty,
                    Location = string.Empty,
                    EventType = string.Empty,
                    Date = System.DateTime.Now,
                    Distance = 0.0
                }
            };

            return View(dashboardBundle);
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
