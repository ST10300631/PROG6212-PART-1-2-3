using System.ComponentModel.DataAnnotations;

namespace RaceDayApi.DTOs
{
    public class CreateEventDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public required string Location { get; set; }

        public double Distance { get; set; }

        [Required]
        public required string EventType { get; set; }
    }
}
