using Microsoft.AspNetCore.Mvc;
using RaceDaySystem.Client.Models.DTOs;
using RaceDaySystem.Client.Services;

namespace RaceDaySystem.Client.Controllers
{
    public class EventsController : Controller
    {
        private readonly RaceDayApiService _events;
        public EventsController(RaceDayApiService events)
        {
            _events = events;
        }
        public async Task<IActionResult> Index()
        {
            var events = await _events.GetEventsAsync();
            return View(events);
        }

        [HttpGet]
        public IActionResult Create()
        {
            string? role = HttpContext.Session.GetString("Role");
            if (role != "Organiser")
                return RedirectToAction("AccessDenied", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // If fields are missing, re-displays the form with warnings
            }

            // Call your working API service endpoint layer to write data to database
            var success = await _events.CreateEventAsync(model);
            if (success)
            {
                return RedirectToAction("Index", "Home"); // Take them back to index page to view it!
            }

            ModelState.AddModelError("", "Could not communicate with the backend service to save event.");
            return View(model);
        }




    }
}
