using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RaceDayApi.Controllers;
using RaceDayApi.DTOs;
using RaceDayApi.Models;
using RaceDayApi.Services;
using RaceEventAPI.Controllers;
using Xunit;


namespace ReaceDayApi
{
    public class EventTests
    {
        [Fact]
        public async Task Create_Event_Should_Succeed()
        {
            var context = DbContextMocker.GetDbContext();

            var controller =
                new EventsController(context);

            var evt = new Event
            {
                Name = "Pretoria Marathon",
                Description = "Annual marathon",
                Location = "Pretoria",
                EventType = "Run",
                Distance = 42.2
            };

            var result =
                await controller.CreateEvent(evt);

            context.Events.Count()
                .Should().Be(1);
        }

        [Fact]
        public async Task Update_Event_Should_Modify_Event()
        {
            var context = DbContextMocker.GetDbContext();

            var evt = new Event
            {
                Name = "Old Name"
            };

            context.Events.Add(evt);

            await context.SaveChangesAsync();

            var controller =
                new EventsController(context);

            evt.Name = "New Name";

            await controller.UpdateEvent(evt.Id, evt);

            context.Events.First()
                .Name.Should().Be("New Name");
        }

        [Fact]
        public void Create_Event_Requires_Organiser_Role()
        {
            var attribute =
                typeof(EventsController)
                .GetMethod("CreateEvent")!
                .GetCustomAttributes(
                    typeof(AuthorizeAttribute), false)
                .Cast<AuthorizeAttribute>()
                .First();

            attribute.Roles.Should()
                     .Be("Organiser");
        }

        [Fact]
        public void Enrolment_Requires_Participant_Role()
        {
            var attribute =
                typeof(EnrolmentsController)
                .GetMethod("Enrol")!
                .GetCustomAttributes(
                    typeof(AuthorizeAttribute), false)
                .Cast<AuthorizeAttribute>()
                .First();

            attribute.Roles.Should()
                     .Be("Participant");
        }
    }
}
