namespace RaceDayApi.DTOs
{
    public class ResultDto
    {
       
            public int EnrolmentId { get; set; }

            public TimeSpan FinishTime { get; set; }

            public int Position { get; set; }

        public String Status { get; set; } = String.Empty;  
        
    }
}
