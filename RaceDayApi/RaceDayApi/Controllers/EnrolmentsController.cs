using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Models;
using RaceDayApi.Data;
using System.Security.Claims;

namespace RaceEventAPI.Controllers
{
    [Route("api/enrolments")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrolmentsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Participant")]
        [HttpPost]
        public async Task<IActionResult> Enrol(
            Enrolment enrolment)
        {
            _context.Enrolments.Add(enrolment);

            await _context.SaveChangesAsync();

            return StatusCode(201, enrolment);
        }

        [Authorize(Roles = "Participant")]
        [HttpGet("my-history")]
        public async Task<IActionResult> MyHistory()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var history = await _context.Enrolments
                .Where(x => x.UserId == userId)
                .Include(x => x.Category)
                .ToListAsync();

            return Ok(history);
        }

        [Authorize(Roles = "Organiser")]
        [HttpGet("/api/events/{eventId}/enrolments")]
        public async Task<IActionResult> EventEntries(int eventId)
        {
            var entries = await _context.Enrolments
                .Include(x => x.User)
                .Include(x => x.Category)
                .Where(x => x.Category.EventId == eventId)
                .ToListAsync();

            return Ok(entries);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var enrolment =
                await _context.Enrolments.FindAsync(id);

            if (enrolment == null)
                return NotFound();

            _context.Enrolments.Remove(enrolment);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Enrolment cancelled"
            });
        }
    }
}