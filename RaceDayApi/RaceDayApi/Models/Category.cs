using RaceDayApi.Models;
using System.ComponentModel.DataAnnotations;

public class Category
{
    public int Id { get; set; }

    [Required]
    public  string Name { get; set; } = null!;

    public  string Description { get; set; } = null!;

    public decimal EntryFee { get; set; }

    public int EventId { get; set; }

    public  Event Event { get; set; } = null!;

    public  ICollection<Enrolment> Enrolments { get; set; } = null!;
}
