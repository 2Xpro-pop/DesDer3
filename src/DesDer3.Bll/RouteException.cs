using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.Models;

namespace DesDer3.Bll;
public class RouteException : Exception
{
    public Route Route
    {
        get;
    }

    public RouteException(Route route, string message): base(message)
    {
        Route = route;
    }
}
