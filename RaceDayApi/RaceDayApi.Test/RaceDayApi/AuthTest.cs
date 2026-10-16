using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RaceDayApi.Tests; // Ensure this matches your ApiTestBase namespace
using Xunit;

namespace RaceDayApi.Test
{
    public class AuthTests : ApiTestBase
    {
        // Use : base(factory) to pass the test server to the base setup
        public AuthTests(WebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async Task Register_WithValidData_Returns201Created()
        {
            // Arrange
            var registerPayload = new
            {
                name = "John Doe",
                email = "john@example.com",
                password = "SecurePassword123!",
                role = "Organiser",
                phone = "1234567890"
            };
            var content = CreateJsonContent(registerPayload); // Inherited helper

            // Act
            var response = await Client.PostAsync("/api/auth/register", content); // Uses inherited 'Client'

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithValidCredentials_Returns200OK()
        {
            // Arrange
            var loginPayload = new
            {
                email = "john@example.com",
                password = "SecurePassword123!"
            };
            var content = CreateJsonContent(loginPayload);

            // Act
            var response = await Client.PostAsync("/api/auth/login", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
