using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface IAuthenticationService
{
    Task<User?> AuthenticateAsync(string userName, string password);
    Task<bool> RegisterUserAsync(User user, string password);
    Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
