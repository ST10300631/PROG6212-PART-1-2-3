using RaceDaySystem.Client.Models;
using System.Net.Http;
using System.Net.Http.Json; // Resolves compiler binding for PostAsJsonAsync extension
using System.Threading.Tasks;

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
            // Use the relative path endpoint. The domain base (https://localhost:7142/) 
            // should live exclusively inside your initialization files.
            var response = await _http.PostAsJsonAsync("api/auth/login", model);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }
    }
}
