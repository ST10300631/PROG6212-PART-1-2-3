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

        public IActionResult Organiser()
        {
            var role = HttpContext.Session.GetString("Role");
            if (!string.Equals(role, "Organiser",
     StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpPost]
        public IActionResult CreateEvent(CreateEventDto dto)
        {
            return RedirectToAction("Organiser", "Account");
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryDto dto)
        {
           return RedirectToAction("Organiser", "Account");
        }

        public IActionResult EditEvent(int id)
        {
            return View();
        }

        public IActionResult DeleteEvent(int id)
        {
            return RedirectToAction("Organiser", "Account");
        }

        public IActionResult EditCategory(int id)
        {
            return View();
        }

        public IActionResult DeleteCategory(int id)
        {
            return RedirectToAction("Organiser", "Account");
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
