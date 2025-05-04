namespace Diabetes.Core.DTOs
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string DisplayRole { get; set; }
        public string InternalRole { get; set; }
    }

}
