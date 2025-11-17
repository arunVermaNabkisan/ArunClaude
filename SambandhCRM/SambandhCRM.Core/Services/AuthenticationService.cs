using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace SambandhCRM.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> AuthenticateAsync(string userName, string password)
    {
        var user = await _userRepository.GetByUserNameAsync(userName);
        if (user == null || !VerifyPassword(password, user.PasswordHash))
        {
            return null;
        }

        // Update last login date
        user.LastLoginDate = DateTime.Now;
        await _userRepository.UpdateAsync(user);

        return user;
    }

    public async Task<bool> RegisterUserAsync(User user, string password)
    {
        // Check if username or email already exists
        var existingUser = await _userRepository.GetByUserNameAsync(user.UserName);
        if (existingUser != null)
        {
            return false;
        }

        existingUser = await _userRepository.GetByEmailAsync(user.Email);
        if (existingUser != null)
        {
            return false;
        }

        // Hash password
        user.PasswordHash = HashPassword(password);
        user.CreatedDate = DateTime.Now;
        user.IsActive = true;

        var userId = await _userRepository.AddAsync(user);
        return userId > 0;
    }

    public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null || !VerifyPassword(oldPassword, user.PasswordHash))
        {
            return false;
        }

        user.PasswordHash = HashPassword(newPassword);
        user.ModifiedDate = DateTime.Now;
        return await _userRepository.UpdateAsync(user);
    }

    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var hashedInput = HashPassword(password);
        return hashedInput == hashedPassword;
    }
}
