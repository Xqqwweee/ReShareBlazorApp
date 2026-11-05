
using ReShareBlazorApp.Shared.DTOs;
using ReShareBlazorApp.Web.Models;

namespace ReShareBlazorApp.Web.ApiServices
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;
        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponse?> LoginAsync(LoginModel model)
        {
            var request = new LoginRequest
            {
                Email = model.Email,
                Password = model.Password
            };
            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/login",
                request
                );
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }
        public async Task<RegisterResponse?> RegisterAsync(RegisterModel model)
        {
            var request = new RegisterRequest
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };
            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/register",
                request
                );
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<RegisterResponse>();
        }
    }
}
