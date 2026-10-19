using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace RaceDayApi.Tests
{
    [TestFixture]
    public class UserProfileTests : ApiTestBase
    {
       

        [Test]
        public async Task GetProfile_WhenUnauthenticated_Returns401Unauthorized()
        {
            // Act
            var response = await Client.GetAsync("/api/users/profile");

            // Assert
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task GetProfile_WhenAuthenticated_Returns200OKWithProfile()
        {
            // Arrange
            AuthenticateClient();

            // Act
            var response = await Client.GetAsync("/api/users/profile");

            // Assert
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
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
            var content = CreateJsonContent(updatePayload);

            // Act
            var response = await Client.PutAsync("/api/users/profile", content);

            // Assert
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
