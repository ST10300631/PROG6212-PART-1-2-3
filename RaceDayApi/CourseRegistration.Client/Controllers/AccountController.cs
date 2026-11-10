using Microsoft.AspNetCore.Mvc;
using RaceDaySystem.Client.Models;
using RaceDaySystem.Client.Models.DTOs;
using RaceDaySystem.Client.Services;
using System.Net.Http;
using System.Net.Http.Json; // Required for PostAsJsonAsync/ReadFromJsonAsync

namespace RaceDaySystem.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthApiService _auth;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(AuthApiService auth, IHttpClientFactory httpClientFactory)
        {
            _auth = auth;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Index() => RedirectToAction("Login");

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // _auth.LoginAsync returns your UserSessionDto object
            var user = await _auth.LoginAsync(model);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            // Save user details to Session
            HttpContext.Session.SetInt32("Id", user.Id);
            HttpContext.Session.SetString("Name", user.Name);
            HttpContext.Session.SetString("Role", user.Role);

            // OPTIONAL: If you want to configure persistent tracking 
            if (model.RememberMe)
            {
                // This sets a persistent cookie expiration value if your 
                // cookie middleware is configured to read from it.
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30),
                    HttpOnly = true,
                    Secure = true
                };
                Response.Cookies.Append("RememberMeUser", model.Email ?? "", options);
            }

            // DYNAMIC LOGIN REDIRECT based on the user's role
            return RedirectUserByRole(user.Role);
        }


        [HttpGet]
        public IActionResult Register() => View();

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var response = await client.PostAsJsonAsync("api/auth/register", registerDto);

            if (response.IsSuccessStatusCode)
            {
                // Assuming your Web API returns the registered user's ID/details upon success
                // so you can instantly log them in and remember them without forcing a second login screen.
                var registeredUser = await response.Content.ReadFromJsonAsync<UserSessionDto>();

                if (registeredUser != null)
                {
                    HttpContext.Session.SetInt32("Id", registeredUser.Id);
                    HttpContext.Session.SetString("Name", registeredUser.Name);
                    HttpContext.Session.SetString("Role", registeredUser.Role);

                    // DYNAMIC REGISTRATION REDIRECT based on their chosen role
                    return RedirectUserByRole(registeredUser.Role);
                }

                // Fallback to login screen if API doesn't return user details
                return RedirectToAction("Login");
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, errorMsg ?? "An error occurred during registration.");
            return View(registerDto);
        }

        private IActionResult RedirectUserByRole(string role)
        {
            switch (role?.Trim().ToLower())
            {
                case "participant":
                    return RedirectToAction("Participant", "Account");

                case "event organiser":
                case "eventorganiser":
                case "organiser": // Fixed: changed to lowercase
                    return RedirectToAction("Organiser", "Account");

                default:
                    return Content($"Role received: '{role}'");
            }
        }


        public async Task<IActionResult> Participant()
        {
            var role = HttpContext.Session.GetString("Role");

            if (!string.Equals(role, "Participant",
                StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");

            var events = await client.GetFromJsonAsync<List<CreateEventDto>>("api/events");

            var model = new ParticipantDashboardViewModelDto
            {
                Events = events ?? new List<CreateEventDto>()
            };

            return View(model);
        }

        // GET: /Account/Organiser
        [HttpGet]
        public async Task<IActionResult> Organiser()
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");

            var events = await client.GetFromJsonAsync<List<CreateEventDto>>("api/events");
            var categories = await client.GetFromJsonAsync<List<CategoryDto>>("api/categories");

            ViewBag.Events = events ?? new List<CreateEventDto>();
            ViewBag.Categories = categories ?? new List<CategoryDto>();

            return View();
        }

        // POST: /Account/CreateEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(CreateEventDto dto)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill in all required event fields.";
                return RedirectToAction("Organiser", "Account");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var response = await client.PostAsJsonAsync("api/events", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Event created successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Failed to create event: {error}";
            }

            return RedirectToAction("Organiser", "Account");
        }
        // POST: /Account/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryDto dto)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill in all required category fields.";
                return RedirectToAction("Organiser", "Account");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");

            // Verify whether your API controller route is "api/categories" or "api/category"
            var response = await client.PostAsJsonAsync("api/categories", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Category created successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Failed to create category: {error}";
            }

            return RedirectToAction("Organiser", "Account");
        }

        // GET: /Account/EditEvent/5
        [HttpGet]
        public async Task<IActionResult> EditEvent(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var ev = await client.GetFromJsonAsync<CreateEventDto>($"api/events/{id}");

            if (ev == null)
            {
                TempData["ErrorMessage"] = "Event not found.";
                return RedirectToAction("Organiser");
            }

            return View(ev);
        }

        // POST: /Account/EditEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(CreateEventDto dto)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var response = await client.PutAsJsonAsync($"api/events/{dto.Id}", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Event updated successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Failed to update event: {error}";
            }

            return RedirectToAction("Organiser");
        }

        // GET: /Account/DeleteEvent/5
        [HttpGet]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var response = await client.DeleteAsync($"api/events/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Event deleted successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Failed to delete event: {error}";
            }

            return RedirectToAction("Organiser");
        }


        // GET: /Account/EditCategory/5
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var categories = await client.GetFromJsonAsync<List<CategoryDto>>("api/categories");
            var category = categories?.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToAction("Organiser");
            }

            return View(category);
        }


        // POST: /Account/EditCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryDto dto)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var response = await client.PutAsJsonAsync($"api/categories/{dto.Id}", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Category updated successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Failed to update category: {error}";
            }

            return RedirectToAction("Organiser");
        }

        // GET: /Account/DeleteCategory/5
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");
            var response = await client.DeleteAsync($"api/categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Category deleted successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Failed to delete category: {error}";
            }

            return RedirectToAction("Organiser");
        }

        // GET: /Account/UpdateProfile
        [HttpGet]
        public IActionResult UpdateProfile()
        {
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UserSessionDto
            {
                Id = userId.Value,
                Name = HttpContext.Session.GetString("Name") ?? string.Empty,
                Role = HttpContext.Session.GetString("Role") ?? string.Empty
            };

            return View(model);
        }

        
        // POST: /Account/UpdateResult
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateResult(int EnrolmentId, string FinishTime, int Position, string Status)
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Participant", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            var client = _httpClientFactory.CreateClient("RaceDayApi");

            var payload = new
            {
                EnrolmentId,
                FinishTime,
                Position,
                Status
            };

            // Adjust "api/results" to match your RaceDayApi controller route
            var response = await client.PostAsJsonAsync("api/results", payload);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Result updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update result.";
            }

            return RedirectToAction("Participant");
        }

        // POST: /Account/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(string Name, string Phone)
        {
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                HttpContext.Session.SetString("Name", Name);
            }

            // Adjust "api/users/{userId}" to match your RaceDayApi profile update endpoint
            var client = _httpClientFactory.CreateClient("RaceDayApi");
            await client.PutAsJsonAsync($"api/users/{userId}", new { Id = userId, Name, Phone });

            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToAction("Participant");
        }


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }

    // Helper matching schema expected back from your backend login/registration API
    public class UserSessionDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
