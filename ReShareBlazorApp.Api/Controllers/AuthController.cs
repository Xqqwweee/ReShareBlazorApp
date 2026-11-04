
using Microsoft.AspNetCore.Mvc;
using ReShareBlazorApp.Api.DTOs;
using ReShareBlazorApp.Api.Interfaces.Services;

namespace ReShareBlazorApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await _authService.RegisterAsync(registerRequest);

            if(result is null)
            {
                return BadRequest(new
                {
                    Message = "Nie udało się utworzyć konta"
                });
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var result = await _authService.LoginAsync(loginRequest);

            if( result is null)
            {
                return Unauthorized(new
                {
                    Message = "Nieprawidłowy email lub hasło"
                });
            }
            return Ok(result);
        }
    }
}
