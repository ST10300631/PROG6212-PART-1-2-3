using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RaceDayApi.Data;
    using RaceDayApi.Models;

    namespace RaceDayApi.Controllers
    {
        [ApiController]
        [Route("api/users")]
        [Authorize]
        public class UsersController : ControllerBase
        {
            private readonly AppDbContext _context;

            public UsersController(AppDbContext context)
            {
                _context = context;
            }

            [HttpGet("profile")]
            public IActionResult GetProfile()
            {
                // Example: return first participant
                var participant = _context.Participants.FirstOrDefault();

                if (participant == null)
                {
                    return NotFound(new
                    {
                        message = "No participant found."
                    });
                }

                return Ok(participant);
            }

            [HttpPut("profile")]
            public IActionResult UpdateProfile(UpdateProfileDto dto)
            {
                var participant = _context.Participants.FirstOrDefault();

                if (participant == null)
                {
                    return NotFound(new
                    {
                        message = "No participant found."
                    });
                }

                participant.FirstName = dto.Name;
                participant.PhoneNumber = long.Parse(dto.Phone);

                _context.SaveChanges();

                return Ok(new
                {
                    updatedUser = participant
                });
            }
        }
    }

}
}


