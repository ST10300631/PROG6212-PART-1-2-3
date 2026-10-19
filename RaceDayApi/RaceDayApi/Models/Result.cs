public class Result
{
    public int Id { get; set; }

    public int EnrolmentId { get; set; }

    public   Enrolment? Enrolment { get; set; } 

    public TimeSpan FinishTime { get; set; }

    public int Position { get; set; }

    public required String Status { get; set; } 
}