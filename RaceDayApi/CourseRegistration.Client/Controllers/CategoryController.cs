using Microsoft.AspNetCore.Mvc;
using RaceDaySystem.Client.Models.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RaceDaySystem.Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Category/Index
        public async Task<IActionResult> Index()
        {
            // Use the named HttpClient configured in Program.cs
            var client = _httpClientFactory.CreateClient("RaceDayApi");
            
            // Call your backend API endpoint that returns categories
            var categories = await client.GetFromJsonAsync<List<CategoryDto>>("api/categories");

            return View(categories ?? new List<CategoryDto>());
        }
    }
}
