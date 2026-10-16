using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayApi.Models
{
    [Table("Events")]
    public class Event
    {
   
            public int EventId { get; set; }

            public int? OrganiserId { get; set; }

            public string Title { get; set; } = string.Empty;

            public string Location { get; set; } = string.Empty;

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public string? Description { get; set; }

            
        }
    }




