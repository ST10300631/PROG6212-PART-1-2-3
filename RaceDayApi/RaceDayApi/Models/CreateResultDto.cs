namespace RaceDayApi.Models
{
    public class CreateResultDto
    {
        public int EnrolmentId { get; set; }
        public TimeSpan FinishTime { get; set; }
        public int Position { get; set; }
    }
}
