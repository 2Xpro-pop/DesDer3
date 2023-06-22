using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.EfIdentity;
using DesDer3.Dal.EfIdentity.Repositories;
using DesDer3.Dal.Models;
using DesDer3.Dal.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DesDer3.Dal.EfIdentity;
internal class EdIdentityUnitOfWork : IUnitOfWork
{
    private readonly DesDer3DbContext _dbContext;
    private readonly UserManager<User> _user;

    public EdIdentityUnitOfWork(UserManager<User> user, DesDer3DbContext dbContext)
    {
        _user = user;
        _dbContext = dbContext;
    }

    public IUserRepository UserRepository => userRepository ??= new UserRepository(_dbContext, _user);
    private IUserRepository? userRepository;

    public IRouteRepository RouteRepository => routeRepository ??= new RouteRepository(_dbContext);
    private IRouteRepository? routeRepository;

    public IPostRepository PostRepository => postRepository ??= new PostRepository(_dbContext);
    private IPostRepository? postRepository;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _dbContext.Dispose();
    }
}
