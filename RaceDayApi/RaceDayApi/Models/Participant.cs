
namespace RaceDayApi.Models
{
    public class Participant
    {
      
        public int ParticipantId { get; set; }

       
        public string FirstName { get; set; } = string.Empty;

      
        public string LastName { get; set; } = string.Empty;

      
        public string Email { get; set; } = string.Empty;

      
        public int PhoneNumber { get; set; }

     
        public DateTime DateOfBirth { get; set; }

    
        public string? ClubName { get; set; }

       
        public string? Role { get; set; }
    }
}
