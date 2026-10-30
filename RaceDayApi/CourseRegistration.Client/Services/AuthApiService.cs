using RaceDaySystem.Client.Models;
using static System.Net.WebRequestMethods;

namespace RaceDaySystem.Client.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _http;

        public AuthApiService(HttpClient http)
        {
            _http = http;
        }
        public async Task<LoginResponse?> LoginAsync(LoginViewModel model)
        {
            // Change this line inside AuthApiService.cs to use the full absolute URL address path
var response = await _http.PostAsJsonAsync("https://localhost:7142/api/auth/login", model);

            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

    }
}
