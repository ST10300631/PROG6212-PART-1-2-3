using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace RaceDayApi.Models
{
    public class Route_detail
    {

        public int RouteId { get; set; }

       
        public int? CategoryId { get; set; }

      
        public string? GpxFileUrl { get; set; }

       
        public string? StartCoordinates { get; set; }

     
        public int? ElevationGainMeters { get; set; }
    }
}
