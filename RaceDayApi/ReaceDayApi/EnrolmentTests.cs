using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RaceDayApi.Controllers;
using RaceDayApi.DTOs;
using RaceDayApi.Models;
using RaceDayApi.Services;
using Xunit;


namespace ReaceDayApi
{
    public class EnrolmentTests
    {
        [Fact]
        public async Task Participant_Can_Enrol()
        {
            var context = DbContextMocker.GetDbContext();

            var enrolment = new Enrolment
            {
                UserId = 1,
                EventId = 1,
                CategoryId = 1
            };

            context.Enrolments.Add(enrolment);

            await context.SaveChangesAsync();

            context.Enrolments.Count()
                   .Should().Be(1);
        }

        [Fact]
        public async Task Enrolment_Should_Link_User_Event_And_Category()
        {
            var context = DbContextMocker.GetDbContext();

            var enrolment = new Enrolment
            {
                UserId = 10,
                EventId = 5,
                CategoryId = 2
            };

            context.Enrolments.Add(enrolment);

            await context.SaveChangesAsync();

            var saved =
                context.Enrolments.First();

            saved.UserId.Should().Be(10);

            saved.EventId.Should().Be(5);

            saved.CategoryId.Should().Be(2);
        }
    }
}
