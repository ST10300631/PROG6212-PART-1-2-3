using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApi.Models
{
    public class Organiser { 

   
    public string Name { get; set; } = string.Empty;

   
    public string Email { get; set; } = string.Empty;

   
    public string ContactNumber { get; set; } = string.Empty;

}
}
