using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace RaceDayApi.Models
{
    [Table("Results")]
    public class Result
    {
        
        public int ResultId { get; set; }

        public int? EntryId { get; set; }

        public TimeSpan? FinishTime { get; set; }

        public int? OverallRank { get; set; }

        public int? CategoryRank { get; set; }

        public string? Status { get; set; }
        public int EnrolmentId { get; internal set; }
        public int Position { get; internal set; }
        public int EventId { get; internal set; }
        public int CategoryId { get; internal set; }
    }
}
