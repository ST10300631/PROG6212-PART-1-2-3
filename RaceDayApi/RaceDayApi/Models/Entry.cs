using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApi.Models
{
    [Table("Entries")]
    public class Entry
    {
       
        public int EntryId { get; set; }

        public int ParticipantId { get; set; }

       
        public int CategoryId { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

       
        public string PaymentStatus { get; set; } = "Pending";

     
        public int? BibNumber { get; set; }
        public int EventId { get; internal set; }
        public string? EmergencyDetails { get; internal set; }
        public string? Status { get; internal set; }
    }
}
