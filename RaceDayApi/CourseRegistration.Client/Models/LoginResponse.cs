namespace RaceDaySystem.Client.Models
{
    public class LoginResponse
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "";
    }
}
