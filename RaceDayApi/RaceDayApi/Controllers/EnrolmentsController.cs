using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Data;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    [ApiController]
    [Route("api/enrolments")]
    [Authorize]
    public class EnrolmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrolmentsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/enrolments
        [HttpPost]
        [Authorize(Roles = "Participant")]
        public IActionResult Enrol([FromBody] CreateEnrolmentDto model)
        {
            var enrolment = new Entry
            {
                EventId = model.EventId,
                CategoryId = model.CategoryId,
                EmergencyDetails = model.EmergencyDetails,
                Status = "Confirmed"
            };

            _context.Entries.Add(enrolment);
            _context.SaveChanges();

            return StatusCode(201, enrolment);
        }

        // GET: /api/enrolments/my-history
        [HttpGet("my-history")]
        [Authorize(Roles = "Participant")]
        public IActionResult MyHistory()
        {
            var history = _context.Entries.ToList();

            return Ok(history);
        }

        // GET: /api/events/{eventId}/enrolments
        [HttpGet("/api/events/{eventId}/enrolments")]
        [Authorize(Roles = "Organiser")]
        public IActionResult EventEnrolments(int eventId)
        {
            var enrolments = _context.Entries
                .Where(e => e.EventId == eventId)
                .ToList();

            return Ok(enrolments);
        }

        // DELETE: /api/enrolments/{id}
        [HttpDelete("{id}")]
        public IActionResult CancelEnrolment(int id)
        {
            var enrolment = _context.Entries.Find(id);

            if (enrolment == null)
            {
                return NotFound(new
                {
                    message = "Enrolment not found."
                });
            }

            _context.Entries.Remove(enrolment);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Enrolment cancelled successfully."
            });
        }
    }
}