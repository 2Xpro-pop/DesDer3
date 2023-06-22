using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.EfIdentity;
using DesDer3.Dal.Models;
using DesDer3.Dal.Repositories;

namespace DesDer3.Dal.EfIdentity.Repositories;
internal class RouteRepository : DbSetBaseRepository<Route>, IRouteRepository
{
    public RouteRepository(DesDer3DbContext context) : base(context)
    {
    }
}
