using System.Collections.Concurrent;
using System.Security.Claims;

namespace SambandhCRM.Web.Services
{
    public class AuthenticationTokenService : IAuthenticationTokenService
    {
        private readonly ConcurrentDictionary<string, (ClaimsPrincipal Principal, AuthenticationProperties Properties, DateTime ExpiresAt)> _tokens = new();

        public AuthenticationTokenService()
        {
            // Start a background task to clean up expired tokens
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    CleanupExpiredTokens();
                }
            });
        }

        public string GenerateToken(ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            var token = Guid.NewGuid().ToString("N");
            var expiresAt = DateTime.UtcNow.AddMinutes(5); // Token valid for 5 minutes

            _tokens[token] = (principal, properties, expiresAt);

            return token;
        }

        public (ClaimsPrincipal? Principal, AuthenticationProperties? Properties) RetrieveAndRemove(string token)
        {
            if (_tokens.TryRemove(token, out var data))
            {
                if (data.ExpiresAt > DateTime.UtcNow)
                {
                    return (data.Principal, data.Properties);
                }
            }

            return (null, null);
        }

        private void CleanupExpiredTokens()
        {
            var expiredTokens = _tokens
                .Where(kvp => kvp.Value.ExpiresAt <= DateTime.UtcNow)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var token in expiredTokens)
            {
                _tokens.TryRemove(token, out _);
            }
        }
    }
}
