using System.ComponentModel.DataAnnotations;

namespace RaceDaySystem.Client.Models.DTOs
{
    public class CreateEventDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

      
        public required string Description { get; set; }

        public DateTime Date { get; set; }

   
        public required string Location { get; set; }

        public double Distance { get; set; }

       
        public required string EventType { get; set; }
    }
}
