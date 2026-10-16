using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Data;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
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

        // GET: /api/events/{eventId}/results
        [HttpGet("events/{EventId}/results")]
        [AllowAnonymous]
        public IActionResult GetResults(int eventId, int? categoryId)
        {
            var results = _context.Results
                .Where(r => r.EventId == eventId);

            if (categoryId.HasValue)
            {
                results = results.Where(r => r.CategoryId == categoryId.Value);
            }

            return Ok(results.ToList());
        }

        // POST: /api/results
        [HttpPost("results")]
        [Authorize(Roles = "Organiser")]
        public IActionResult AddResult([FromBody] CreateResultDto model)
        {
            var result = new Result
            {
                EnrolmentId = model.EnrolmentId,
                FinishTime = model.FinishTime,
                Position = model.Position
            };

            _context.Results.Add(result);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetResults),
                new { eventId = result.EventId },
                result);
        }

        // PUT: /api/results/{id}
        [HttpPut("results/{id}")]
        [Authorize(Roles = "Organiser")]
        public IActionResult UpdateResult(
            int id,
            [FromBody] UpdateResultDto model)
        {
            var result = _context.Results.Find(id);

            if (result == null)
                return NotFound();

            result.FinishTime = model.FinishTime;
            result.Position = model.Position;

            _context.SaveChanges();

            return Ok(result);
        }
    }
}