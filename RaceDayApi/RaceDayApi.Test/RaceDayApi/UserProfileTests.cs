using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RaceDayApi.Tests; // Ensure this matches where ApiTestBase lives
using Xunit;

namespace RaceDayApi.Test
{
    public class UserProfileTests : ApiTestBase
    {
        public UserProfileTests(WebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async Task GetProfile_WhenUnauthenticated_Returns401Unauthorized()
        {
            // Act
            var response = await Client.GetAsync("/api/users/profile");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetProfile_WhenAuthenticated_Returns200OKWithProfile()
        {
            // Arrange
            AuthenticateClient();

            // Act
            var response = await Client.GetAsync("/api/users/profile");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProfile_WithValidData_Returns200OK()
        {
            // Arrange
            AuthenticateClient();
            var updatePayload = new
            {
                name = "John Updated",
                phone = "0987654321",
                emergencyContact = "Jane Doe"
            };
            var content = CreateJsonContent(updatePayload); // Handled by ApiTestBase

            // Act
            var response = await Client.PutAsync("/api/users/profile", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}


