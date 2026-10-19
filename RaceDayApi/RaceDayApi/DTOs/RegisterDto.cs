namespace RaceDayApi.DTOs
{
    public class RegisterDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Phone { get; set; } = string.Empty;
        public required string Role { get; set; }
    }
}
