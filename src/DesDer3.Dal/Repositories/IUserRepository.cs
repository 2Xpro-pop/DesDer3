using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.Models;

namespace DesDer3.Dal.Repositories;
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Creates a user with the specified <paramref name="user"/> and password.
    /// </summary>
    /// <param name="user">The user object containing user details.</param>
    /// <param name="password">The password to associate with the user.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task CreateUserAsync(User user, string password);
}
