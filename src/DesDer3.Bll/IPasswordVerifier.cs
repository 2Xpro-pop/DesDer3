using DesDer3.Dal;
using DesDer3.Dal.Models;

namespace DesDer3.Bll;

/// <summary>
/// The interface IPasswordVerifier should be implemented in the Presentation Layer; 
/// otherwise, the regular IPasswordHasher<User> will be used for password verification.
/// </summary>
public interface IPasswordVerifier
{
    Task<bool> VerifyPasswordAsync(User user, string hashedPassword, string password);
}
