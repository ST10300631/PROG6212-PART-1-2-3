using System.ComponentModel.DataAnnotations;

namespace RaceDayApi.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; } = null!;

        public double Distance { get; set; }

        [Required]
        public string EventType { get; set; } = null!; // Run, Walk, Cycle

        public int OrganiserId { get; set; }

        public User Organiser { get; set; } = null!;

        public ICollection<Category> Categories { get; set; } = null!;
    }
}
