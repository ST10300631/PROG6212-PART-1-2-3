using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{

    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {



        private static List<Participant> participants = new()
        {
            new Participant
            {
                ParticipantId = 1,
                FirstName = "Sipho",
                LastName = "Dlamini",
                Email = "sipho.dlamini@email.com",
                PhoneNumber = 821234567,
                DateOfBirth = new DateTime(1995, 03, 12),
                ClubName = "Soweto Harriers",
                Role = "Participant"
            },

            new Participant
            {
                ParticipantId = 2,
                FirstName = "Sarah",
                LastName = "Smith",
                Email = "sarah.smith@email.com",
                PhoneNumber = 719876543,
                DateOfBirth = new DateTime(1988, 07, 24),
                ClubName = "Cape Town Striders",
                Role = "Organiser"
            },

            new Participant
            {
                ParticipantId = 3,
                FirstName = "Tendai",
                LastName = "Moyo",
                Email = "tendai.moyo@email.com",
                PhoneNumber = 635551234,
                DateOfBirth = new DateTime(2001, 11, 05),
                ClubName = null,
                Role = "Participant"
            },

            new Participant
            {
                ParticipantId = 4,
                FirstName = "Jessica",
                LastName = "Botha",
                Email = "jess.botha@email.com",
                PhoneNumber = 844449876,
                DateOfBirth = new DateTime(1992, 01, 30),
                ClubName = "Durban Athletic Club",
                Role = "Participant"
            },

            new Participant
            {
                ParticipantId = 5,
                FirstName = "David",
                LastName = "Ndlovu",
                Email = "d.ndlovu@email.com",
                PhoneNumber = 761112222,
                DateOfBirth = new DateTime(1985, 09, 18),
                ClubName = null,
                Role = "Organiser"
            }
        };


        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok(new
            {
                id = 1,
                name = "John Doe",
                email = "john@example.com",
                role = "Participant"
            });
        }

        [HttpPut("profile")]
        public IActionResult UpdateProfile(UpdateProfileDto dto)
        {
            return Ok(new
            {
                updatedUser = dto
            });
        }
    }
}


