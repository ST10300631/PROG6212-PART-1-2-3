namespace RaceDaySystem.Client.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        // Add this to bind to your "Remember Me" checkbox in the View
        public bool RememberMe { get; set; }
    }
}
