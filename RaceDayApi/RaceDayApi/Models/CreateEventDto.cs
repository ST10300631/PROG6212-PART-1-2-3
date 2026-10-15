namespace RaceDayApi.Models
{
    public class CreateEventDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public required string Location { get; set; }
    }
}
