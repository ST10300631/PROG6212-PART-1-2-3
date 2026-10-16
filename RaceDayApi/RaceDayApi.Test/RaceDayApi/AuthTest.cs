using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace RaceDayApi.Tests
{
    public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_Returns_Ok()
        {
            var response = await _client.PostAsJsonAsync(
                "/api/auth/login",
                new
                {
                    Email = "test@email.com",
                    Password = "Password123!"
                });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Register_Returns_Created()
        {
            var response = await _client.PostAsJsonAsync(
                "/api/auth/register",
                new
                {
                    Name = "John Doe",
                    Email = "john@email.com",
                    Password = "Password123!",
                    Role = "Participant",
                    Phone = "0821234567"
                });

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}