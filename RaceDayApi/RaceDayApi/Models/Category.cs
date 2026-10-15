using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApi.Models
{
    public class Category
    {
      
        public int CategoryId { get; set; }

       
        public int? EventId { get; set; }

       
        public string? Name { get; set; }

      
        public decimal? EntryFee { get; set; }

       
        public decimal? DistanceKm { get; set; }

       
        public TimeSpan? StartTime { get; set; }
    }
}
