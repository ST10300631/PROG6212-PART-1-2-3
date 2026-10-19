using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Models;
using RaceDayApi.Data;
using System.Security.Claims;
using RaceDayApi.DTOs;

namespace RaceEventAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ResultsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResultsController(AppDbContext context)
        {
            _context = context;
        }

        // Helper to verify if the currently logged-in Organiser owns the event
        private async Task<bool> IsUserEventOrganiser(int eventId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int organiserId))
                return false;

            return await _context.Events.AnyAsync(e => e.Id == eventId && e.OrganiserId == organiserId);
        }

        [HttpGet("events/{eventId}/results")]
        public async Task<IActionResult> GetResults(int eventId)
        {
            var results = await _context.Results
                .Include(r => r.Enrolment)
                .Where(r => r.Enrolment!.Category.EventId == eventId)
                .OrderBy(r => r.Position)
                .ToListAsync();

            return Ok(results);
        }

       /* [Authorize(Roles = "Organiser")]
        [HttpGet("events/{eventId}/enrolments")]
        public async Task<IActionResult> ViewEnrolments(int eventId)
        {
            if (!await IsUserEventOrganiser(eventId))
                return Forbid();

            var enrolments = await _context.Enrolments
                .Include(e => e.User)
                .Include(e => e.Category)
                .Where(e => e.EventId == eventId)
                .ToListAsync();

            return Ok(enrolments);
        }
       */
        [Authorize(Roles = "Organiser")]
        [HttpPost("results")]
        public async Task<IActionResult> AddResult(ResultDto dto)
        {
            // Validate that the Enrolment target exists and belongs to an event owned by this Organiser
            var enrolment = await _context.Enrolments
                .FirstOrDefaultAsync(e => e.Id == dto.EnrolmentId);

            if (enrolment == null)
                return NotFound("Enrolment not found.");

            if (!await IsUserEventOrganiser(enrolment.EventId))
                return Forbid();

            var result = new Result
            {
                EnrolmentId = dto.EnrolmentId,
                FinishTime = dto.FinishTime,
                Position = dto.Position,
                Status = dto.Status // Added status support if present in DTO
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResults), new { eventId = enrolment.EventId }, result);
        }

        [Authorize(Roles = "Organiser")]
        [HttpPut("results/{id}")]
        public async Task<IActionResult> UpdateResult(int id, ResultDto dto)
        {
            var existing = await _context.Results
                .Include(r => r.Enrolment)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (existing == null)
                return NotFound();

            // Guard: Ensure organizer owns the race event for this result record
            if (!await IsUserEventOrganiser(existing.Enrolment!.EventId))
                return Forbid();

            existing.FinishTime = dto.FinishTime;
            existing.Position = dto.Position;
            existing.Status = dto.Status;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [Authorize(Roles = "Participant")]
        [HttpGet("my-results")]
        public async Task<IActionResult> MyResults()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("Invalid User ID Claim.");

            var results = await _context.Results
                .Include(r => r.Enrolment)
                .ThenInclude(e => e!.Event)
                .Where(r => r.Enrolment!.UserId == userId)
                .ToListAsync();

            return Ok(results);
        }
    }
}
