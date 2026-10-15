using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Principal;

namespace RaceDayApi.Models
{
    public class Result
    {
        
        public int ResultId { get; set; }

        public int? EntryId { get; set; }

        public TimeSpan? FinishTime { get; set; }

        public int? OverallRank { get; set; }

        public int? CategoryRank { get; set; }

        public string? Status { get; set; }
    }
}
