using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Models;
using RaceDayApi.Data;


namespace RaceEventAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToListAsync();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventItem = await _context.Events
                .Include(e => e.Categories)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventItem == null)
                return NotFound();

            return Ok(eventItem);
        }

        [Authorize(Roles = "Organiser")]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event eventModel)
        {
            _context.Events.Add(eventModel);
            await _context.SaveChangesAsync();

            return StatusCode(201, eventModel);
        }

        [Authorize(Roles = "Organiser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(
            int id,
            Event eventModel)
        {
            var existing = await _context.Events.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.Name = eventModel.Name;
            existing.Description = eventModel.Description;
            existing.Date = eventModel.Date;
            existing.Location = eventModel.Location;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        [Authorize(Roles = "Organiser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
                return NotFound();

            _context.Events.Remove(eventItem);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Event deleted"
            });
        }
    }
}