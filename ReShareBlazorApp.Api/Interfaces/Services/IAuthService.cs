using ReShareBlazorApp.Api.DTOs;

namespace ReShareBlazorApp.Api.Interfaces.Services
{
    public interface IAuthService
    {
        Task<RegisterResponse?> RegisterAsync(RegisterRequest registerRequest);
        Task<LoginResponse?> LoginAsync(LoginRequest loginRequest);
    }
}
