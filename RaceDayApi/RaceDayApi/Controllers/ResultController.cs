using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Models;

namespace RaceDayApi.Models.Controllers
{
    [ApiController]
    [Route("api")]
    public class ResultsController : ControllerBase
    {
        // GET: /api/events/{eventId}/results
        [HttpGet("Event/{EventId}/Result")]
        [AllowAnonymous]
        public IActionResult GetResults(
            int EventId,
            int? CategoryId)
        {
            return Ok(new[]
            {
                new
                {
                    Rank = 1,
                    Name = "John Doe",
                    Time = "00:18:34"
                }
            });
        }

        // POST: /api/results
        [HttpPost("Result")]
        [Authorize(Roles = "Organiser")]
        public IActionResult AddResult(
            [FromBody] CreateResultDto model)
        {
            return StatusCode(201, model);
        }

        // PUT: /api/results/{id}
        [HttpPut("Result/{id}")]
        [Authorize(Roles = "Organiser")]
        public IActionResult UpdateResult(
            int id,
            [FromBody] UpdateResultDto model)
        {
            return Ok(new
            {
                UpdatedResult = model
            });
        }
    }
}