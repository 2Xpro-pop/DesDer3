using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal;
using DesDer3.Dal.Models;
using Microsoft.AspNetCore.Identity;

namespace DesDer3.Bll.Internal;
internal class DefaultPasswordVerifier: IPasswordVerifier
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public DefaultPasswordVerifier(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public Task<bool> VerifyPasswordAsync(User user, string hashedPassword, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);

        return Task.FromResult((result == PasswordVerificationResult.Success) || 
               (result == PasswordVerificationResult.SuccessRehashNeeded));
    }
}
