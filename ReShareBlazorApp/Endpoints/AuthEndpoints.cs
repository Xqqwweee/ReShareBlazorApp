using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReShareBlazorApp.Shared.DTOs;
using ReShareBlazorApp.Web.ApiServices;
using ReShareBlazorApp.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReShareBlazorApp.Web.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (
                [FromForm] LoginModel model,
                HttpContext context,
                AuthApiService authApiService,
                IConfiguration configuration) =>
            {
                var result = await authApiService.LoginAsync(model);
                if (result is null)
                {
                    return Results.Redirect("/login?error=1");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                configuration["Jwt:Key"]!
                )
            ),
                    ClockSkew = TimeSpan.Zero
                };
                ClaimsPrincipal principal;
                try
                {
                    principal = tokenHandler.ValidateToken(
                        result.Token,
                        validationParameters,
                        out _
                        );
                }
                catch (SecurityTokenException ex)
                {
                    return Results.Redirect("/login?error=1");
                }
                var identity = (ClaimsIdentity)principal.Identity!;
                identity.AddClaim(new Claim("access_token", result.Token));
                var jwt = tokenHandler.ReadJwtToken(result.Token);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = jwt.ValidTo
                };
                await context.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    properties
                    );
                return Results.Redirect("/");
            });
            app.MapPost("/auth/logout", async
                (HttpContext context) =>
            {
                await context.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );
                return Results.LocalRedirect("/login");
            });
                
        }
    }
}
