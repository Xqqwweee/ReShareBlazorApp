using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReShareBlazorApp.Api.Data;
using ReShareBlazorApp.Api.DTOs;
using ReShareBlazorApp.Api.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReShareBlazorApp.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<RegisterResponse?> RegisterAsync(RegisterRequest registerRequest)
        {
            User user = new ()
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                CreatedAt = DateTime.Now,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if(!result.Succeeded)
            {
                return null;
            }
            return new RegisterResponse
            {
                Message = "Konto zostało pomyślnie utworzone"
            };

        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null || !user.IsActive) return null;
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if(!isPasswordValid) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
                );
            var expiresAt = DateTime.Now.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])
                );
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
                );
            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt,
                Email = user.Email ?? ""
            };
        }
    }
}
