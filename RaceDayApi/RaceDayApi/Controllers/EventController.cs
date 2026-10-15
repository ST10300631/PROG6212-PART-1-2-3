using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        static private List<Event> events = new List<Event>
        {      

    new Event
    {
        EventId = 1,
        OrganiserId = 1,
        Title = "Soweto 10K Classic",
        Location = "Soweto, Johannesburg",
        StartDate = new DateTime(2026, 4, 12),
        EndDate = new DateTime(2026, 4, 12),
        Description = "An annual 10km road running event touring iconic heritage sites."
    },
    new Event
    {
        EventId = 2,
        OrganiserId = 2,
        Title = "Cape Town Cycle Tour Mini",
        Location = "Green Point, Cape Town",
        StartDate = new DateTime(2026, 5, 24),
        EndDate = new DateTime(2026, 5, 25),
        Description = "A weekend community cycling event for local clubs and amateur riders."
    },
    new Event
    {
        EventId = 3,
        OrganiserId = 3,
        Title = "Durban Night Run",
        Location = "Beachfront, Durban",
        StartDate = new DateTime(2026, 6, 15),
        EndDate = new DateTime(2026, 6, 15),
        Description = "A scenic 5km and 10km evening race along the Durban promenade."
    },
    new Event
    {
        EventId = 101,
        OrganiserId = 101,
        Title = "Johannesburg Autumn Trail",
        Location = "Modderfontein Reserve",
        StartDate = new DateTime(2026, 7, 4),
        EndDate = new DateTime(2026, 7, 5),
        Description = "A two-day multi-stage trail running challenge covering 15km daily."
    },
    new Event
    {
        EventId = 102,
        OrganiserId = 102,
        Title = "Gauteng Criterium Series",
        Location = "Kyalami Grand Prix Circuit",
        StartDate = new DateTime(2026, 8, 20),
        EndDate = new DateTime(2026, 8, 20),
        Description = "Fast-paced closed-circuit road bike racing across multiple skill tiers."
    }
};

        // GET: api/Event
        [HttpGet]
        public ActionResult<Event> GetEvents()
        {
            
            return Ok(events);
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public ActionResult<Event> GetEvent(int id)
        {
            // Renamed variable to 'eventItem' and added '.Find(id)'
            var eventItem = events.Find(e => e.EventId == id);

            if (eventItem == null)
            {
                return NotFound(new { message = $"Event with ID {id} not found." });
            }

            return Ok(eventItem);
        }

    }


}


