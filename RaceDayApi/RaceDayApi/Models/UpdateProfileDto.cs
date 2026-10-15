namespace RaceDayApi.Models
{
    public class UpdateProfileDto
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string EmergencyContact { get; set; }
    }
}
