using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDaySystem.Client.Models.DTOs;
using System.Threading.Tasks;

namespace RaceDaySystem.Client.Controllers
{
    [AllowAnonymous]
    public class ResultController : Controller
    {
        // GET: Result/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Initializes an empty model instance to bind cleanly with your view
            var model = new ResultDto();
            return View(model);
        }

        // POST: Result/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ResultDto model)
        {
            if (!ModelState.IsValid)
            {
                // If validation rules fail, return the view with current inputs and validation errors
                return View(model);
            }

            try
            {
                // TODO: Add your service layer or API client submission call here
                // e.g., await _resultService.CreateResultAsync(model);

                // Redirect user back to the results tracking dashboard upon successful save
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving the race record. Please try again.");
                return View(model);
            }
        }

        // GET: Result/Result
        [HttpGet]
        public IActionResult Result()
        {
            return View();
        }

    }
}
