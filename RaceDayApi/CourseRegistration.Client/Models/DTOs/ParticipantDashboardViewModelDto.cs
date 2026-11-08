namespace RaceDaySystem.Client.Models.DTOs
{
    public class ParticipantDashboardViewModelDto
    {
        public List<CreateEventDto> Events { get; set; } = new();

        public List<ResultDto> Results { get; set; } = new();

        public UpdateProfileDto Profile { get; set; } = new();
    }
}
