namespace ReShareBlazorApp.Api.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public string Email { get; set; } = null!;
    }
}
