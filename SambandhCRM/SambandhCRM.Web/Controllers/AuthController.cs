using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SambandhCRM.Web.Services;

namespace SambandhCRM.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationTokenService _tokenService;

    public AuthController(IAuthenticationTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpGet("signin")]
    public async Task<IActionResult> SignIn([FromQuery] string token)
    {
        try
        {
            // Validate and retrieve the stored authentication data
            var (principal, properties) = _tokenService.ValidateToken(token);

            if (principal == null)
            {
                return Redirect("/login?error=invalid_token");
            }

            // Convert custom properties to ASP.NET Core AuthenticationProperties
            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = properties.IsPersistent,
                ExpiresUtc = properties.ExpiresUtc
            };

            // Sign in the user
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);

            // Redirect to dashboard
            return Redirect("/dashboard");
        }
        catch (Exception ex)
        {
            return Redirect($"/login?error={Uri.EscapeDataString(ex.Message)}");
        }
    }

    [HttpGet("signout")]
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/login");
    }
}