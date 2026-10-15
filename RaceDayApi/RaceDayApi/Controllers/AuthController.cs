using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Data;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto model)
        {
            // Example: save a participant/user

            // _context.Participants.Add(participant);
            // _context.SaveChanges();

            return StatusCode(201, new
            {
                token = "jwt-token",
                user = model
            });
        }
    }
}