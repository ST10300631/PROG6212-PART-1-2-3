using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using RaceDaySystem.Client.Models;


namespace RaceDaySystem.Client.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Public gallery page
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RaceDayApi");

            var images = await client.GetFromJsonAsync<List<Gallery>>(
                "api/gallery");

            return View(images ?? new List<Gallery>());
        }

        // Upload page
        [HttpGet]
        public IActionResult Upload()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Participant" && role != "Organiser")
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Participant" && role != "Organiser")
            {
                return Unauthorized();
            }

            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select an image.";
                return RedirectToAction(nameof(Index));
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");

            using var content = new MultipartFormDataContent();

            using var stream = file.OpenReadStream();

            var fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "file", file.FileName);

            var response = await client.PostAsync(
                "api/gallery/upload",
                content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] =
                    "Image uploaded successfully!";
            }
            else
            {
                TempData["ErrorMessage"] =
                    "Upload failed.";
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
