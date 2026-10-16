using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RaceDayApi.Tests; // Ensure this matches your ApiTestBase namespace
using Xunit;

namespace RaceDayApi.Test
{
    public class EventTests : ApiTestBase
    {
        public EventTests(WebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async Task CreateEvent_WhenUserIsPublicRole_Returns403Forbidden()
        {
            // Arrange
            AuthenticateClient(token: "regular-user-token");
            var newEvent = new { title = "Tech Meetup", description = "Networking event", date = "2026-11-20" };
            var content = CreateJsonContent(newEvent);

            // Act
            var response = await Client.PostAsync("/api/events", content);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task CreateEvent_WhenUserIsOrganiser_Returns201Created()
        {
            // Arrange
            AuthenticateClient(token: "organiser-token");
            var newEvent = new { title = "Tech Meetup", description = "Networking event", date = "2026-11-20" };
            var content = CreateJsonContent(newEvent);

            // Act
            var response = await Client.PostAsync("/api/events", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
