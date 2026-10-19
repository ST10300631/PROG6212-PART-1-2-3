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
    public class AuthTests
    {
        [Fact]
        public async Task Register_Should_Create_User()
        {
            var context = DbContextMocker.GetDbContext();

            var controller =
                new AuthController(context, null!);

            var dto = new RegisterDto
            {
                Name = "John",
                Email = "john@test.com",
                Password = "Password123",
                Role = "Participant",
                Phone = "0821234567"
            };

            var result = await controller.Register(dto);

            context.Users.Count().Should().Be(1);
        }

        [Fact]
        public async Task Login_Should_Return_Ok_When_Valid()
        {
            var context = DbContextMocker.GetDbContext();

            context.Users.Add(new User
            {
                Name = "John",
                Email = "john@test.com",
                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword("123456"),
                Phone = "0821234567",
                Role = "Participant"
            });

            await context.SaveChangesAsync();

            var jwtMock = new Mock<JwtService>();

            jwtMock.Setup(x => x.GenerateToken(It.IsAny<User>()))
                   .Returns("fake-token");

            var controller =
                new AuthController(context, jwtMock.Object);

            var dto = new LoginDto
            {
                Email = "john@test.com",
                Password = "123456"
            };

            var result = await controller.Login(dto);

            result.Should()
                  .BeOfType<OkObjectResult>();
        }

    }
}