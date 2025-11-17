using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SambandhCRM.Web.Services;

namespace SambandhCRM.Web.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationTokenService _authTokenService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IAuthenticationTokenService authTokenService,
            ILogger<AccountController> logger)
        {
            _authTokenService = authTokenService;
            _logger = logger;
        }

        [HttpGet("CompleteLogin")]
        public async Task<IActionResult> CompleteLogin([FromQuery] string token, [FromQuery] string returnUrl = "/dashboard")
        {
            try
            {
                var (principal, authProperties) = _authTokenService.RetrieveAndRemove(token);

                if (principal == null || authProperties == null)
                {
                    _logger.LogWarning("Invalid or expired authentication token");
                    return Redirect("/login?error=invalid_token");
                }

                var cookieAuthProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = authProperties.IsPersistent,
                    ExpiresUtc = authProperties.ExpiresUtc
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    cookieAuthProperties);

                _logger.LogInformation("User {UserName} signed in successfully", principal.Identity?.Name);

                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing login");
                return Redirect("/login?error=signin_failed");
            }
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/login");
        }
    }
}
