
using Microsoft.AspNetCore.Mvc;
using RaceDayApi.Data;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
       
            private readonly AppDbContext _context;

            public EventController(AppDbContext context)
            {
                _context = context;
            }

            // GET: api/Event
            [HttpGet]
            public IActionResult GetEvents()
            {
                return Ok(_context.Events.ToList());
            }

            // GET: api/Event/5
            [HttpGet("{id}")]
            public IActionResult GetEvent(int id)
            {
                var eventItem = _context.Events.Find(id);

                if (eventItem == null)
                {
                    return NotFound(new
                    {
                        message = $"Event with ID {id} not found."
                    });
                }

                return Ok(eventItem);
            }

        // POST: Add Create Event
        [HttpPost]
        public IActionResult CreateEvent(Event eventItem)
        {
            _context.Events.Add(eventItem);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetEvent),
                new { id = eventItem.EventId },
                eventItem);
        }


        // POST: Add Update Event
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, Event updatedEvent)
        {
            var eventItem = _context.Events.Find(id);

            if (eventItem == null)
                return NotFound();

            eventItem.Title = updatedEvent.Title;
            eventItem.Location = updatedEvent.Location;
            eventItem.StartDate = updatedEvent.StartDate;
            eventItem.EndDate = updatedEvent.EndDate;
            eventItem.Description = updatedEvent.Description;

            _context.SaveChanges();

            return Ok(eventItem);
        }

        

        // Delete Add Delete Event
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var eventItem = _context.Events.Find(id);

            if (eventItem == null)
                return NotFound();

            _context.Events.Remove(eventItem);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Event deleted successfully"
            });
        }


    }

    }





