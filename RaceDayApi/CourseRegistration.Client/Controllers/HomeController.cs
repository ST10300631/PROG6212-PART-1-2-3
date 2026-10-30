using Microsoft.AspNetCore.Mvc;
using RaceDaySystem.Client.Models;
using RaceDaySystem.Client.Models.DTOs;
using RaceDaySystem.Client.Services;
using System.Diagnostics;

namespace RaceDaySystem.Client.Controllers
{
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
        private readonly RaceDayApiService _apiService; // Add your service here

       
      

        public HomeController(ILogger<HomeController> logger, RaceDayApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
{
    // Fetch the list from your API service
    var events = await _apiService.GetEventsAsync(); 
    
    // Pass the list into the View to stop the null error
    return View(events); 
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
