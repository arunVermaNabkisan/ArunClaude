using System.Security.Claims;

namespace SambandhCRM.Web.Services
{
    public interface IAuthenticationTokenService
    {
        string GenerateToken(ClaimsPrincipal principal, AuthenticationProperties properties);
        (ClaimsPrincipal? Principal, AuthenticationProperties? Properties) RetrieveAndRemove(string token);
    }

    public class AuthenticationProperties
    {
        public bool IsPersistent { get; set; }
        public DateTimeOffset? ExpiresUtc { get; set; }
    }
}
