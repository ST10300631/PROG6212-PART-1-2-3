using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace RaceDayApi.Tests
{
    [TestFixture]
    public class AuthTests : ApiTestBase
    {
        [Test]
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
            var content = CreateJsonContent(registerPayload);

            // Act
            var response = await Client.PostAsync("/api/auth/register", content);

            // Assert
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var responseString = await response.Content.ReadAsStringAsync();
            NUnit.Framework.Assert.That(responseString, Does.Contain("token"));
            NUnit.Framework.Assert.That(responseString, Does.Contain("user"));
        }

        [Test]
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
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
