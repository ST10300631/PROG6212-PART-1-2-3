using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace RaceDayApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }
        public  string Phone { get; set; } = null!;

        public required string Role { get; set; }

        public  ICollection<Event>? Events { get; set; }
    }
}
