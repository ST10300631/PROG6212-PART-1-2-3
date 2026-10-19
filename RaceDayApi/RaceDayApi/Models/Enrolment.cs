using RaceDayApi.Models;

public class Enrolment
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public  User User { get; set; } = null!;

    public int EventId { get; set; }
    public  Event Event { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public DateTime EnrolmentDate { get; set; }
        = DateTime.Now;
}