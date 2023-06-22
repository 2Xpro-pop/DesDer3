using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.Repositories;

namespace DesDer3.Dal;
public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository
    {
        get;
    }

    public IRouteRepository RouteRepository
    {
        get;
    }

    public IPostRepository PostRepository
    {
        get;
    }
}
