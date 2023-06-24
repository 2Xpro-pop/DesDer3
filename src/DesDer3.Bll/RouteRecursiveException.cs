using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.Models;

namespace DesDer3.Bll;
public class RouteRecursiveException : RouteException
{
    public RouteRecursiveException(Route route, string message) : base(route, message)
    {

    }
}
