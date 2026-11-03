using Microsoft.AspNetCore.Identity;
using ReShareBlazorApp.Data;
using ReShareBlazorApp.Interfaces.Services;

namespace ReShareBlazorApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    }
}
