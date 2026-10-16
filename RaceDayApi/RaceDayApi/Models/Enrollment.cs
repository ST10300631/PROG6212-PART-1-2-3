using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Security.Principal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayApi.Models
{
    [Table("Enrollment")]
    public class Enrollment
    {

        public int EntryId { get; set; }

      
        public int ParticipantId { get; set; }

        public int CategoryId { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public string PaymentStatus { get; set; } = "Pending";

     
        public int? BibNumber { get; set; }

    }
}
