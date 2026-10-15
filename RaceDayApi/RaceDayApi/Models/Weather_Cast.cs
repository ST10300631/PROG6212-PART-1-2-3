using Microsoft.AspNetCore.Http.HttpResults;

namespace RaceDayApi.Models
{
    public class Weather_Cast
    {
      
        public int WeatherId { get; set; }

    
        public int? CategoryId { get; set; }

   
        public decimal? TemperatureCelsius { get; set; }

        public decimal? WindSpeedKph { get; set; }

      
        public string? ConditionsDescription { get; set; }

      
        public DateTime? LastUpdated { get; set; }

    }
}
