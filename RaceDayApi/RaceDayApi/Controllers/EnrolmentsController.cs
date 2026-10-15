using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RaceDayApi.Models.Controllers
{
    [ApiController]
    [Route("api/Enrolments")]
    [Authorize]
    public class EnrolmentsController : ControllerBase
    {
        // POST: /api/Enrolments
        [HttpPost]
        [Authorize(Roles = "Participant")]
        public IActionResult Enrol(
            [FromBody] CreateEnrolmentDto model)
        {
            return StatusCode(201, new
            {
                enrolmentId = 1,
                status = "Confirmed"
            });
        }

        // GET: /api/Enrolments/my-history
        [HttpGet("Result")]
        [Authorize(Roles = "Participant")]
        public IActionResult MyHistory()
        {
            return Ok(new[]
            {
                new
                {
                    Title = "Soweto 10K Classi",
                    Category = "10km",
                    Status = "Completed"
                }
            });
        }

        // GET: /api/events/{eventId}/enrolments
        [HttpGet("/api/events/{EventId}/Enrolments")]
        [Authorize(Roles = "Organiser")]
        public IActionResult EventEnrolments(int eventId)
        {
            return Ok(new[]
            {
                new
                {
                    Participant = "John Doe",
                    Category = "5km"
                }
            });
        }

        // DELETE: /api/enrolments/{id}
        [HttpDelete("{id}")]
        public IActionResult CancelEnrolment(int id)
        {
            return Ok(new
            {
                message = "Enrolment cancelled"
            });
        }
    }
}
