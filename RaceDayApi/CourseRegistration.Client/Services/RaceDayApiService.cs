
using RaceDaySystem.Client.Models.DTOs;
using System.Net.Http.Json;

namespace RaceDaySystem.Client.Services
{
    public class RaceDayApiService
    {
        private readonly HttpClient _httpClient;

        // Inject HttpClient to handle backend API calls
        public RaceDayApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Change the return type from string? to List<CreateEventDto>
        public async Task<List<CreateEventDto>> GetEventsAsync()
        {
            try
            {
                // Replace "api/events" with your exact backend endpoint route if different
                var events = await _httpClient.GetFromJsonAsync<List<CreateEventDto>>("api/events");

                return events ?? new List<CreateEventDto>();
            }
            catch (Exception)
            {
                // Return an empty list as a safeguard if the API is offline or unreachable
                return new List<CreateEventDto>();
            }
        }

            // Add this method inside your RaceDayApiService class block
public async Task<bool> CreateEventAsync(CreateEventDto model)
        {
            try
            {
                // Sends a POST request with the form data to your backend API
                var response = await _httpClient.PostAsJsonAsync("api/events", model);

                // Returns true if the database saved it successfully (status code 200-299)
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // Returns false to trigger your controller's "Could not save the event" error alert
                return false;
            }
        }

    }
}

