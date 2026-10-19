using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace RaceDayApi.Tests
{
    [TestFixture]
    public class EventTests : ApiTestBase
    {
       

        [Test]
        public async Task BrowseEvents_WithQueryFilters_Returns200OK()
        {
            // Act
            var response = await Client.GetAsync("/api/events?search=tech&location=London");

            // Assert
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task CreateEvent_WhenUserIsPublicRole_Returns403Forbidden()
        {
            // Arrange
            AuthenticateClient(token: "regular-user-token");
            var newEvent = new { title = "Tech Meetup", description = "Networking event", date = "2026-11-20" };
            var content = CreateJsonContent(newEvent);

            // Act
            var response = await Client.PostAsync("/api/events", content);

            // Assert
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [Test]
        public async Task CreateEvent_WhenUserIsOrganiser_Returns201Created()
        {
            // Arrange
            AuthenticateClient(token: "organiser-token");
            var newEvent = new { title = "Tech Meetup", description = "Networking event", date = "2026-11-20" };
            var content = CreateJsonContent(newEvent);

            // Act
            var response = await Client.PostAsync("/api/events", content);

            // Assert
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task CancelEvent_WhenUserIsOwner_Returns200OK()
        {
            // Arrange
            AuthenticateClient(token: "owner-token");
            string eventId = "123";

            // Act
            var response = await Client.DeleteAsync($"/api/events/{eventId}");

            // Assert
            NUnit.Framework.Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
