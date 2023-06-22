using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.EfIdentity;
using DesDer3.Dal.Models;
using DesDer3.Dal.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DesDer3.Dal.EfIdentity.Repositories;
internal class UserRepository : DbSetBaseRepository<User>, IUserRepository
{
    private readonly UserManager<User> _userManager;
    public UserRepository(DesDer3DbContext context, UserManager<User> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public Task CreateUserAsync(User user, string password) => _userManager.CreateAsync(user, password);

    public override Task AddAsync(User model) => _userManager.CreateAsync(model);
    public override Task DeleteAsync(User model) => _userManager.DeleteAsync(model);
    public override Task UpdateAsync(User model) => _userManager.UpdateAsync(model);

}
