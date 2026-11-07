using Microsoft.AspNetCore.Mvc;
using RaceDaySystem.Client.Models;
using RaceDaySystem.Client.Models.DTOs;
using RaceDaySystem.Client.Services;
using System.Net.Http;

namespace RaceDaySystem.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthApiService _auth;
        private readonly IHttpClientFactory _httpClientFactory;

      

        public AccountController(AuthApiService auth , IHttpClientFactory httpClientFactory)
        {
            _auth = auth;
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Account/
        public IActionResult Index()
        {
            return View("Login");
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _auth.LoginAsync(model);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetInt32("Id", user.Id);
            HttpContext.Session.SetString("Name", user.Name);
            HttpContext.Session.SetString("Role", user.Role);

            // CHANGE THIS LINE RIGHT HERE:
            // This forwards the user to CategoryController's Index action
            return RedirectToAction("Index", "Home");
        }


        // POST: /Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            // Clears all keys out of the active user session data store
            HttpContext.Session.Clear();

            // Redirects the logged-out user back to the public homepage layout view
            return RedirectToAction("Index", "Home");
        }

        // 1. GET: /Account/Register
        // This displays your new form to the user
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // 2. POST: /Account/Register
        // This processes the submitted form and forwards it to your Web API backend
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto); // Returns form with validation errors if inputs are invalid
            }

            // Create an HTTP client configured to speak to your backend API project
            var client = _httpClientFactory.CreateClient("RaceDayApi");

            // Send the registration payload to your API's registration endpoint
            var response = await client.PostAsJsonAsync("api/auth/register", registerDto);

            if (response.IsSuccessStatusCode)
            {
                // Redirect the user to the Login page upon successful registration
                return RedirectToAction("Login");
            }

            // If the API returned an error (e.g., email already exists), display it on the form
            var errorMsg = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, errorMsg ?? "An error occurred during registration.");

            return View(registerDto);
        }

    }
}

