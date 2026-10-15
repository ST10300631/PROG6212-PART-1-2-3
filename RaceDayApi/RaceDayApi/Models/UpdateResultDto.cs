namespace RaceDayApi.Models
{
    public class UpdateResultDto
    {
        public TimeSpan FinishTime { get; set; }
        public int Position { get; set; }
        public required string Status { get; set; }
    }
}
